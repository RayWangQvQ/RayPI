using Microsoft.EntityFrameworkCore;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;

namespace Ray.Infrastructure.Repository.EfCore
{
    public interface IEfRepository<TEntity, TDbContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity
        where TDbContext : EfDbContext<TDbContext>
    {
        /// <summary>
        /// EF的数据库上下文
        /// </summary>
        TDbContext DbContext { get; }

        /// <summary>
        /// EF的DbSet数据集
        /// </summary>
        DbSet<TEntity> DbSet { get; }
    }

    public interface IEfRepository<TEntity, TKey, TDbContext> : IEfRepository<TEntity, TDbContext>, IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TDbContext : EfDbContext<TDbContext>
    {

    }
}
