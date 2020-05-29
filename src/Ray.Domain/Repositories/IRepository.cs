using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ray.Domain.Entities;
using Ray.Infrastructure.Helpers.Page;

namespace Ray.Domain.Repositories
{
    /// <summary>
    /// 定义一个【仓储】
    /// </summary>
    public interface IRepository
    {

    }

    /// <summary>
    /// 无主键的实体仓储interface
    /// 【职责：实体的CRUD】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IQueryRepository<TEntity>, ICommandRepository<TEntity>
        where TEntity : class, IEntity
    {

    }

    /// <summary>
    /// 有泛型主键的实体仓储interface
    /// 【职责：实体的CRUD】
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity>, IQueryRepository<TEntity, TKey>, ICommandRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
