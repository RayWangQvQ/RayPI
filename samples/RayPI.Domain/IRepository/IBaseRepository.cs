using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ray.Domain.Repositories;
using RayPI.Domain.Entity;
using RayPI.Infrastructure.Treasury.Enums;
using RayPI.Infrastructure.Treasury.Models;


namespace RayPI.Domain.IRepository
{
    public interface IBaseRepository<TEntity> : IIntegratedRepository<TEntity>
        where TEntity : EntityBase, new()
    {
        /*
        #region 添加
        long Add(TEntity entity);
        /// <summary>批量新增</summary>
        /// <param name="entityList">实体集合</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> Add(IEnumerable<TEntity> entityList);
        #endregion

        #region 更新
        /// <summary>更新实体</summary>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>        
        /// <exception cref="T:System.NotImplementedException"></exception>
        void Update(TEntity entity);

        /// <summary>批量修改</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        void Update(IQueryable<TEntity> entityList);
        #endregion

        #region 删除
        #region 物理移除
        /// <summary>物理移除</summary>
        /// <param name="item">实体</param>
        void Remove(TEntity entity);

        /// <summary>批量物理移除</summary>
        /// <param name="entityList">批量实体</param>
        void Remove(IQueryable<TEntity> entityList);

        /// <summary>批量物理移除</summary>
        /// <param name="filter">移除条件</param>
        void Remove(Expression<Func<TEntity, bool>> filter);
        /// <summary>物理移除</summary>
        /// <param name="id">主键</param>
        void Remove(long id);
        #endregion

        #region 逻辑删除(实质是更新：更新实体的IsDeleted字段和DeleteTime字段)
        /// <summary>逻辑删除</summary>
        /// <param name="item">The item.</param>
        void Delete(TEntity entity);

        /// <summary>逻辑删除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        void Delete(IQueryable<TEntity> entityList);

        /// <summary>逻辑删除</summary>
        /// <param name="filter">移除条件</param>
        void Delete(Expression<Func<TEntity, bool>> filter);

        /// <summary>逻辑删除</summary>
        /// <param name="id">The item.</param>
        void Delete(long id);
        #endregion
        #endregion
        */
    }
}
