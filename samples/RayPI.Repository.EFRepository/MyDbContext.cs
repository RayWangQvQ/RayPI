using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RayPI.Domain.Entity;
using RayPI.Infrastructure.Treasury.Extensions;
using RayPI.Infrastructure.Treasury.Helpers;
using Ray.Infrastructure.EFRepository;
using MediatR;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Ray.Domain;
using Ray.Domain.OperatorInfo;

namespace RayPI.Repository.EFRepository
{
    public class MyDbContext : EfDbContext<MyDbContext>
    {
        private readonly IEntityOperatorInfoBuilder _entityOperatorInfoBuilder;
        private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public MyDbContext()
        {

        }
        public MyDbContext(DbContextOptions options)
            : base(options, null, null)
        {
        }

        public MyDbContext(DbContextOptions options, IEntityOperatorInfoBuilder entityOperatorInfoBuilder)
            : base(options, null, null)
        {
            _entityOperatorInfoBuilder = entityOperatorInfoBuilder;
        }

        public MyDbContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus, IEntityOperatorInfoBuilder entityOperatorInfoBuilder)
            : base(options, mediator, capBus)
        {
            _entityOperatorInfoBuilder = entityOperatorInfoBuilder;
        }

        #region OnConfiguring
        /// <summary>
        /// 配置数据库
        /// （该方法内配置会覆盖构造函数中传入的DbContextOptions）
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //日志：
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }
        #endregion

        #region OnModelCreating
        /// <summary>
        /// 映射类所在程序集
        /// </summary>
        protected override Assembly EntityTypeConfigurationAssembly => Assembly.GetExecutingAssembly();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //映射关系
            base.OnModelCreating(modelBuilder);

            //设置全局默认的数据库敢纲要（不设置的话默认为dbo）
            modelBuilder.HasDefaultSchema("ray");

            //初始化一条数据
            modelBuilder.Entity<ArticleEntity>().HasData(new ArticleEntity
            {
                Id = 1,
                Title = "这是一条初始化的数据",
                SubTitle = "来自DbContext的OnModelCreating",
                Content = "这是内容"
            });
        }
        #endregion

        /// <summary>Sets the base.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="isAdd">if set to <c>true</c> [is add].</param>
        private void SetEntityBaseInfo<TAggregateRoot>(TAggregateRoot item, bool isAdd = true) where TAggregateRoot : EntityBase
        {
            var entityOperatorInfoBuilder = _entityOperatorInfoBuilder ?? new DefaultEntityOperatorInfoBuilder();
            IEntityOperatorInfo entityOperatorInfo = entityOperatorInfoBuilder.Build();
            if (isAdd)//添加
            {
                if (item.Id <= 0L)
                    item.Id = IdGenerateHelper.NewId;
                item.CreateId = entityOperatorInfo?.CreateId;
                item.CreateTime = entityOperatorInfo?.CreateTime;
                item.CreateName = entityOperatorInfo?.CreateName;
            }
            else//编辑
            {
                item.UpdateId = entityOperatorInfo?.UpdateId;
                item.UpdateTime = entityOperatorInfo?.UpdateTime;
                item.UpdateName = entityOperatorInfo?.UpdateName;
            }
        }

        #region 查询
        /// <summary>查询所有匹配项</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="exceptDeleted">排除被逻辑删除的</param>
        /// <returns>IQueryable<TAggregateRoot></returns>
        public IQueryable<TAggregateRoot> GetAllMatching<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter = null, bool exceptDeleted = true) where TAggregateRoot : EntityBase
        {
            IQueryable<TAggregateRoot> source;
            if (filter == null)
                source = this.RayDbSet<TAggregateRoot>().Where(x => true);
            else
                source = this.RayDbSet<TAggregateRoot>().Where(filter);

            if (exceptDeleted)
                source = source.Where(x => x.IsDeleted == false);
            return source;
        }
        #endregion

        #region 添加
        public long Add<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : EntityBase
        {
            if (entity == null) return -1;
            this.SetEntityBaseInfo(entity);
            this.RayDbSet<TAggregateRoot>().Add(entity);
            return entity.Id;
        }
        /// <summary>批量新增</summary>
        /// <param name="tAggregateRoots">实体集合</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> Add<TAggregateRoot>(IEnumerable<TAggregateRoot> entityList) where TAggregateRoot : EntityBase
        {
            var ids = new List<long>();
            if (entityList.Any())
            {
                entityList.Each(x => this.Add(x));
            }
            return ids;
        }
        #endregion

        #region 更新
        /// <summary>更新实体</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>
        public void Update<TAggregateRoot>(TAggregateRoot entity, params Expression<Func<TAggregateRoot, object>>[] ignoreFileds) where TAggregateRoot : EntityBase, new()
        {
            List<string> source = new List<string>()
            {
                "CreateID",
                "CreateName",
                "CreateTime"
            };
            ignoreFileds.Each(x =>
            {
                MemberInfo member = x.GetMember();
                string propertyName = member?.Name;
                if (!string.IsNullOrEmpty(propertyName) && !source.Exists(s => s == propertyName))
                    source.Add(propertyName);
            });
            if (entity != null)
            {
                this.SetEntityBaseInfo(entity, false);
                this.SetModified(entity);
            }
            List<string> list = source.Where(m => !string.IsNullOrWhiteSpace(m)).Select(m => m.Trim()).ToList();
            foreach (MemberInfo property in entity.GetType().GetProperties())
            {
                string name = property.Name;
                if (list.Contains(name, StringComparer.OrdinalIgnoreCase))
                    this.Entry(entity).Property(name).IsModified = false;
            }
        }

        /// <summary>修改集合</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        public void Update<TAggregateRoot>(IQueryable<TAggregateRoot> tAggregateRoots) where TAggregateRoot : EntityBase, new()
        {
            tAggregateRoots.Each(x => this.Update(x));
        }
        #endregion

        #region 删除
        #region 物理移除
        /// <summary>物理删除一个对象</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void Remove<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase
        {
            if (item == null) return;
            SetDeleted(item);
            Set<TAggregateRoot>().Remove(item);
        }

        /// <summary>批量物理移除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public void Remove<TAggregateRoot>(IQueryable<TAggregateRoot> entityList) where TAggregateRoot : EntityBase
        {
            entityList.Each(x => this.Remove(x));
        }

        /// <summary>批量物理移除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void Remove<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter) where TAggregateRoot : EntityBase
        {
            RayDbSet<TAggregateRoot>().Where(filter).Each(x => this.Remove(x));
        }
        #endregion

        #region 逻辑删除(实质是更新：更新实体的IsDeleted字段和DeleteTime字段)
        /// <summary>根据条件逻辑删除</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void Delete<TAggregateRoot>(TAggregateRoot item) where TAggregateRoot : EntityBase, new()
        {
            item.IsDeleted = true;
            this.Update(item);
        }

        /// <summary>逻辑删除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public void Delete<TAggregateRoot>(IQueryable<TAggregateRoot> entityList) where TAggregateRoot : EntityBase, new()
        {
            entityList.Each(x => this.Delete(entityList));
        }

        /// <summary>逻辑删除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void Delete<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter) where TAggregateRoot : EntityBase, new()
        {
            IQueryable<TAggregateRoot> list = this.RayDbSet<TAggregateRoot>().Where(filter);
            list.Each(x =>
            {
                x.IsDeleted = true;
                this.Update(x, new Expression<Func<TAggregateRoot, object>>[0]);
            });
        }
        #endregion
        #endregion
    }
}
