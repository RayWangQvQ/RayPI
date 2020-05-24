using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ray.Domain.RepositoryInterfaces;
using Ray.Infrastructure.EFRepository;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Treasury.Enums;
using RayPI.Infrastructure.Treasury.Models;


namespace RayPI.Repository.EFRepository.Repository
{
    /// <summary>
    /// 仓储层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IntegratedRepository<T, MyDbContext>, IBaseRepository<T>
        where T : EntityBase, new()
    {
        private readonly MyDbContext _myDbContext;

        public BaseRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _myDbContext = myDbContext;
        }

        #region 添加
        public long Add(T entity)
        {
            //todo:设置操作人信息
            _myDbContext.Add(entity);
            _myDbContext.SaveChanges();
            return entity.Id;
        }
        /// <summary>批量新增</summary>
        /// <param name="tAggregateRoots">实体集合</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> Add(IEnumerable<T> entityList)
        {
            //todo
            _myDbContext.Add(entityList);
            _myDbContext.SaveChanges();
            return entityList.Select(x => x.Id);
        }
        #endregion

        #region 更新
        /// <summary>更新实体</summary>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>        /// <exception cref="T:System.NotImplementedException"></exception>
        public virtual void Update(T entity)
        {
            _myDbContext.Update(entity);
            _myDbContext.SaveChanges();
        }

        /// <summary>批量修改</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public virtual void Update(IQueryable<T> entityList)
        {
            _myDbContext.Update(entityList);
            _myDbContext.SaveChanges();
        }
        #endregion

        #region 删除
        #region 物理移除
        /// <summary>物理移除</summary>
        /// <param name="item">实体</param>
        public virtual void Remove(T entity)
        {
            _myDbContext.Remove(entity);
            _myDbContext.SaveChanges();
        }

        /// <summary>批量物理移除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public void Remove(IQueryable<T> entityList)
        {
            _myDbContext.RemoveRange(entityList);
            _myDbContext.SaveChanges();
        }

        /// <summary>批量物理移除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void Remove(Expression<Func<T, bool>> filter)
        {
            _myDbContext.Remove(Find(filter));
            _myDbContext.SaveChanges();
        }
        /// <summary>物理移除</summary>
        /// <param name="id">主键</param>
        public virtual void Remove(long id)
        {
            this.Remove(x => x.Id == id);
        }
        #endregion

        #region 逻辑删除(实质是更新：更新实体的IsDeleted字段和DeleteTime字段)
        /// <summary>逻辑删除</summary>
        /// <param name="item">The item.</param>
        public virtual void Delete(T entity)
        {
            _myDbContext.Delete(entity);
            _myDbContext.SaveChanges();
        }

        /// <summary>逻辑删除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public void Delete(IQueryable<T> entityList)
        {
            _myDbContext.Delete(entityList);
            _myDbContext.SaveChanges();
        }

        /// <summary>逻辑删除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void Delete(Expression<Func<T, bool>> filter)
        {
            _myDbContext.Delete(filter);
            _myDbContext.SaveChanges();
        }
        /// <summary>逻辑删除</summary>
        /// <param name="id">The item.</param>
        public virtual void Delete(long id)
        {
            this.Delete(x => x.Id == id);
        }
        #endregion
        #endregion
    }
}
