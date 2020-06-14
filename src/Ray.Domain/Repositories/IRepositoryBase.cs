using System;
using System.Collections.Generic;
using System.Text;
using Ray.Domain.Entities;

namespace Ray.Domain.Repositories
{
    /// <summary>
    /// 无主键的实体仓储interface
    /// 【职责：实体的CRUD】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryBase<TEntity> : IQueryRepository<TEntity>, ICommandRepository<TEntity>
        where TEntity : class, IEntity
    {

    }

    /// <summary>
    /// 有泛型主键的实体仓储interface
    /// 【职责：实体的CRUD】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity>, IQueryRepository<TEntity, TKey>, ICommandRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
