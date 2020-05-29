using Microsoft.EntityFrameworkCore;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;

namespace Ray.Infrastructure.Repository.EfCore
{
    public interface IEfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// EF的数据库上下文
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// EF的DbSet数据集
        /// </summary>
        DbSet<TEntity> DbSet { get; }
    }

    public interface IEfRepository<TEntity, TKey> : IEfRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
