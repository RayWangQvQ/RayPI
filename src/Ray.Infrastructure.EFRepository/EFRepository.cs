using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ray.Domain;
using Ray.Domain.RepositoryInterfaces;

namespace Ray.Infrastructure.EFRepository
{
    /// <summary>
    /// EF仓储抽象基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEFContext"></typeparam>
    public abstract class EFRepository<TEntity, TEFContext> : IRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
        where TEFContext : EFContext
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="context"></param>
        public EFRepository(TEFContext context)
        {
            this.EFContext = context;
        }

        /// <summary>
        /// 上下文
        /// </summary>
        protected virtual TEFContext EFContext { get; set; }

        /// <summary>
        /// 工作单元
        /// </summary>
        public virtual IUnitOfWork UnitOfWork => EFContext;

        public virtual TEntity Add(TEntity entity)
        {
            return EFContext.Add(entity).Entity;
        }

        public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Add(entity));
        }

        public virtual TEntity Update(TEntity entity)
        {
            return EFContext.Update(entity).Entity;
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Update(entity));
        }

        public virtual bool Remove(Entity entity)
        {
            EFContext.Remove(entity);
            return true;
        }

        public virtual Task<bool> RemoveAsync(Entity entity)
        {
            return Task.FromResult(Remove(entity));
        }
    }


    public abstract class Repository<TEntity, TKey, TDbContext> : EFRepository<TEntity, TDbContext>, IRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot where TDbContext : EFContext
    {
        public Repository(TDbContext context) : base(context)
        {
        }

        public virtual TEntity Get(TKey id)
        {
            return EFContext.Find<TEntity>(id);
        }

        public virtual async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await EFContext.FindAsync<TEntity>(id, cancellationToken);
        }

        public virtual bool Delete(TKey id)
        {
            var entity = EFContext.Find<TEntity>(id);
            if (entity == null)
            {
                return false;
            }
            EFContext.Remove(entity);
            return true;
        }

        public virtual async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await EFContext.FindAsync<TEntity>(id, cancellationToken);
            if (entity == null)
            {
                return false;
            }
            EFContext.Remove(entity);
            return true;
        }
    }

}
