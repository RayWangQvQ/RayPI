using Microsoft.EntityFrameworkCore;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;

namespace Ray.Repository.EfCore.Repositories
{
    public interface IEfRepository<TEntity> : IBaseRepository<TEntity>
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

    public interface IEfRepository<TEntity, TKey> : IEfRepository<TEntity>, IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

    }
}
