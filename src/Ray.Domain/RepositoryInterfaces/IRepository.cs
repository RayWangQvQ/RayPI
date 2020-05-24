using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ray.Domain;
using Ray.Infrastructure.Helpers.Page;

namespace Ray.Domain.RepositoryInterfaces
{
    /// <summary>
    /// 无主键的实体仓储interface
    /// 【职责：实体的CRUD】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        #region 查
        /// <summary>
        /// 根据条件获取聚合根信息
        /// （无则返回null）
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns>TAggregateRoot.</returns>
        TEntity Find(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 查询所有符合条件的
        /// </summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        IQueryable<TEntity> GetAllMatching(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TK">The type of the tk.</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="sortOrder">排序（正序、倒序etc）</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>PageResult&lt;TAggregateRoot&gt;.</returns>
        PageResult<TEntity> GetListPaged<TK>(int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TK>> orderByExpression = null,
            SortEnum sortOrder = SortEnum.Asc);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TK">The type of the tk.</typeparam>
        /// <param name="queryAggres">查询集合</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">查询条件</param>
        /// <param name="orderByExpression">排序规则</param>
        /// <param name="sortOrder">排序枚举</param>
        /// <returns>PageResult&lt;TAggregateRoot&gt;.</returns>
        PageResult<TEntity> GetListPaged<TK>(IQueryable<TEntity> queryAggres,
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TK>> orderByExpression = null,
            SortEnum sortOrder = SortEnum.Asc);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>true=存在，false=不存在</returns>
        bool Any(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>System.Int32.</returns>
        int Count(Expression<Func<TEntity, bool>> filter);
        #endregion

        #region 增
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="tAggregateRoots">实体集合</param>
        IEnumerable<TEntity> Add(IEnumerable<TEntity> tAggregateRoots);

        /// <summary>
        /// 异步添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        #endregion

        #region 改
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFields">忽略部分字段更新</param>
        void Update(TEntity entity, params Expression<Func<TEntity, dynamic>>[] ignoreFields);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="tAggregateRoots">批量实体</param>
        /// <param name="ignoreFields"></param>
        void Update(IQueryable<TEntity> tAggregateRoots,
            params Expression<Func<TEntity, dynamic>>[] ignoreFields);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="item"></param>
        /// <param name="updateFields">需要更新的字段</param>
        void UpdateWithField(TEntity item,
            params Expression<Func<TEntity, dynamic>>[] updateFields);

        /// <summary>
        /// 修改集合
        /// </summary>
        /// <param name="tAggregateRoots">The t aggregate roots.</param>
        /// <param name="updateFields"></param>
        void UpdateWithField(IQueryable<TEntity> tAggregateRoots,
            params Expression<Func<TEntity, dynamic>>[] updateFields);

        /// <summary>
        /// 异步编辑实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据赋值的实体进行更新
        /// </summary>
        /// <param name="filter">更新条件</param>
        /// <param name="value">更新字段值</param>
        void Update(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TEntity>> value);

        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="item">实体</param>
        void UpSert(TEntity entity);
        #endregion

        #region 删
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Remove(Entity entity);

        /// <summary>
        /// 批量移除
        /// </summary>
        /// <param name="tAggregateRoots">批量实体</param>
        void Remove(IQueryable<TEntity> tAggregateRoots);

        /// <summary>
        /// 根据条件筛选移除
        /// </summary>
        /// <param name="filter">移除条件</param>
        void Remove(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveAsync(Entity entity);
        #endregion
    }

    /// <summary>
    /// 有泛型主键的实体仓储interface
    /// 【职责：实体的CRUD】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity>
        where TEntity : Entity<TKey>, IAggregateRoot
    {
        #region 查询
        /// <summary>
        /// 根据聚合根的id查 或者 联查一些附属信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>TAggregateRoot.</returns>
        TEntity Find(TKey id);
        /// <summary>
        /// 根据主键异步获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default);
        #endregion

        #region 删
        /// <summary>
        /// 根据主键移除
        /// </summary>
        /// <param name="id">主键</param>
        void Remove(TKey id);

        /// <summary>
        /// 根据主键异步删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(TKey id, CancellationToken cancellationToken = default);
        #endregion
    }

    public interface IIntegratedRepository<TEntity> : IRepository<TEntity, long>
        where TEntity : IntegratedEntity, IAggregateRoot
    {
        #region 查询
        /// <summary>
        /// 查询实体信息
        /// </summary>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        IQueryable<TEntity> GetAll(bool isIgnoreDelete);

        /// <summary>
        /// 不分页查询
        /// </summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        IQueryable<TEntity> GetAllMatching(Expression<Func<TEntity, bool>> filter, bool isIgnoreDelete);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TK">The type of the tk.</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">查询条件</param>
        /// <param name="orderByExpression">排序条件</param>
        /// <param name="sortOrder">排序（正序、倒序etc）</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>PageResult&lt;TAggregateRoot&gt;.</returns>
        PageResult<TEntity> GetListPaged<TK>(int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> filter,
            bool isIgnoreDelete,
            Expression<Func<TEntity, TK>> orderByExpression = null,
            SortEnum sortOrder = SortEnum.Asc);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>true=存在，false=不存在</returns>
        bool Any(Expression<Func<TEntity, bool>> filter, bool isIgnoreDelete);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="specification">查询条件</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>System.Int32.</returns>
        int Count(Expression<Func<TEntity, bool>> filter, bool isIgnoreDelete);

        /// <summary>
        /// 根据聚合根的id查 或者 联查一些附属信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="isIgnoreDelete">是否忽略已逻辑删除的数据</param>
        /// <returns>TAggregateRoot.</returns>
        TEntity Find(long id, bool isIgnoreDelete);
        #endregion

        #region 增
        #endregion

        #region 改

        #endregion

        #region 删
        #region 物理移除
        #endregion

        #region 逻辑删除
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id">The key.</param>
        void Delete(long id);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="item">The item.</param>
        void Delete(TEntity item);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="filter">移除条件</param>
        void Delete(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="tAggregateRoots">批量实体</param>
        void Delete(IQueryable<TEntity> tAggregateRoots);
        #endregion
        #endregion
    }
}
