using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//
using RayPI.Domain.Entity;
using RayPI.Infrastructure.Treasury.Enums;
using RayPI.Infrastructure.Treasury.Models;


namespace RayPI.Domain.IRepository
{
    public interface IBaseRepository<T> where T : EntityBase, new()
    {
        #region 查询
        /// <summary>查询所有匹配项</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="exceptDeleted">排除被逻辑删除的</param>
        /// <returns>IQueryable<T></T></returns>
        IQueryable<T> GetAllMatching(Expression<Func<T, bool>> filter = null, bool exceptDeleted = true);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="exceptDeleted"></param>
        /// <param name="filterExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        PageResult<T> GetPageList<TK>(int pageIndex, int pageSize,
            bool exceptDeleted = true, Expression<Func<T, bool>> filterExpression = null,
            Expression<Func<T, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc);

        /// <summary>判断是否存在</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="exceptDeleted">是否忽略已逻辑删除的数据</param>
        /// <returns>true=存在，false=不存在</returns>
        bool Any(Expression<Func<T, bool>> filter, bool exceptDeleted);

        /// <summary>根据条件获取</summary>
        /// <param name="filter">查询条件</param>
        /// <returns>T.</returns>
        T Find(Expression<Func<T, bool>> filter, bool exceptDeleted = true);

        /// <summary>
        /// 根据Id获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exceptDeleted"></param>
        /// <returns></returns>
        T FindById(long id, bool exceptDeleted = true);
        #endregion

        #region 添加
        long Add(T entity);
        /// <summary>批量新增</summary>
        /// <param name="entityList">实体集合</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        IEnumerable<long> Add(IEnumerable<T> entityList);
        #endregion

        #region 更新
        /// <summary>更新实体</summary>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>        
        /// <exception cref="T:System.NotImplementedException"></exception>
        void Update(T entity);

        /// <summary>批量修改</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        void Update(IQueryable<T> entityList);
        #endregion

        #region 删除
        #region 物理移除
        /// <summary>物理移除</summary>
        /// <param name="item">实体</param>
        void Remove(T entity);

        /// <summary>批量物理移除</summary>
        /// <param name="entityList">批量实体</param>
        void Remove(IQueryable<T> entityList);

        /// <summary>批量物理移除</summary>
        /// <param name="filter">移除条件</param>
        void Remove(Expression<Func<T, bool>> filter);
        /// <summary>物理移除</summary>
        /// <param name="id">主键</param>
        void Remove(long id);
        #endregion

        #region 逻辑删除(实质是更新：更新实体的IsDeleted字段和DeleteTime字段)
        /// <summary>逻辑删除</summary>
        /// <param name="item">The item.</param>
        void Delete(T entity);

        /// <summary>逻辑删除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        void Delete(IQueryable<T> entityList);

        /// <summary>逻辑删除</summary>
        /// <param name="filter">移除条件</param>
        void Delete(Expression<Func<T, bool>> filter);

        /// <summary>逻辑删除</summary>
        /// <param name="id">The item.</param>
        void Delete(long id);
        #endregion
        #endregion
    }
}
