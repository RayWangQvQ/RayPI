using System.Threading;
using System.Threading.Tasks;
using Ray.Domain;

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

        #region 增
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);
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
        /// 编辑实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);
        /// <summary>
        /// 异步编辑实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        #endregion

        #region 删
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Remove(Entity entity);
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(Entity entity);
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
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(TKey id);
        /// <summary>
        /// 根据主键异步获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
        #endregion

        #region 删
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(TKey id);
        /// <summary>
        /// 根据主键异步删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        #endregion
    }
}
