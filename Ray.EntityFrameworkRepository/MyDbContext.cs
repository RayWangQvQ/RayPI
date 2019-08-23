using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
//
using Microsoft.EntityFrameworkCore;
//
using RayPI.Treasury.Models;
using RayPI.Treasury.Helpers;
using RayPI.Treasury.Interfaces;
using RayPI.Treasury.Extensions;
using RayPI.Domain.Entity;

namespace Ray.EntityFrameworkRepository
{
    public class MyDbContext : DbContext
    {
        private readonly TokenModel _tokenModel;
        public MyDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public MyDbContext(DbContextOptions options, TokenModel tokenModel)
            : base(options)
        {
            _tokenModel = tokenModel;
        }

        /// <summary>
        /// 配置实体生成表属性
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        /// <summary>Creates the set.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        public DbSet<TAggregateRoot> CreateSet<TAggregateRoot>() where TAggregateRoot : class
        {
            return this.Set<TAggregateRoot>();
        }

        /// <summary>Sets the modified.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="entity">The entity.</param>
        public void SetModified<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : class
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>Sets the deleted.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="entity">The entity.</param>
        public void SetDeleted<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : class
        {
            this.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>保存</summary>
        /// <exception cref="T:System.Data.Entity.Validation.DbEntityValidationException">Entity Validation Failed - errors follow:\n +
        /// sb.ToString()</exception>
        public void MySaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>Sets the base.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="isAdd">if set to <c>true</c> [is add].</param>
        private void SetEntityBaseInfo<TAggregateRoot>(TAggregateRoot item, bool isAdd = true) where TAggregateRoot : EntityBase
        {
            IEntityBaseAutoSetter entityBaseAutoSetter = item.AutoSetter ?? new OperateSetter(_tokenModel);
            if (isAdd)//添加
            {
                if (item.Id <= 0L)
                    item.Id = IdGenerateHelper.NewId;
                item.CreateId = entityBaseAutoSetter.CreateId;
                item.CreateTime = entityBaseAutoSetter.CreateTime;
                item.CreateName = entityBaseAutoSetter.CreateName;
            }
            else//编辑
            {
                item.UpdateId = entityBaseAutoSetter.UpdateId;
                item.UpdateTime = entityBaseAutoSetter.UpdateTime;
                item.UpdateName = entityBaseAutoSetter.UpdateName;
            }
        }

        #region 查询
        /// <summary>查询所有匹配项</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="exceptDeleted">排除被逻辑删除的</param>
        /// <returns>IQueryable<TAggregateRoot></T></returns>
        public IQueryable<TAggregateRoot> GetAllMatching<TAggregateRoot>(Expression<Func<TAggregateRoot, bool>> filter = null, bool exceptDeleted = true) where TAggregateRoot : EntityBase
        {
            IQueryable<TAggregateRoot> source;
            if (filter == null)
                source = this.CreateSet<TAggregateRoot>().Where(x => true);
            else
                source = this.CreateSet<TAggregateRoot>().Where(filter);

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
            CreateSet<TAggregateRoot>().Add(entity);
            return entity.Id;
        }
        /// <summary>批量新增</summary>
        /// <param name="tAggregateRoots">实体集合</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> Add<TAggregateRoot>(IEnumerable<TAggregateRoot> entityList) where TAggregateRoot : EntityBase
        {
            List<long> ids = new List<long>();
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
            CreateSet<TAggregateRoot>().Where(filter).Each(x => this.Remove(x));
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
            IQueryable<TAggregateRoot> list = this.CreateSet<TAggregateRoot>().Where(filter);
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
