using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;

namespace Ray.Infrastructure.Repository.EfCore
{
    /// <summary>
    /// EF仓储抽象基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class EfRepository<TEntity, TDbContext> : RepositoryBase<TEntity>, IEfRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : EfDbContext<TDbContext>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="context"></param>
        public EfRepository(TDbContext context)
        {
            this.DbContext = context;
        }

        /// <summary>
        /// 上下文
        /// </summary>
        protected virtual TDbContext DbContext { get; }
        DbContext IEfRepository<TEntity>.DbContext => DbContext.As<DbContext>();

        /// <summary>
        /// DbSet数据集
        /// </summary>
        public virtual DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        /// <summary>
        /// 工作单元
        /// （这里直接利用EF的DbContext实现）
        /// </summary>
        public virtual IUnitOfWork UnitOfWork => DbContext;

        #region 查
        public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public override async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().ToListAsync(GetCancellationToken(cancellationToken))
                : await DbSet.ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public override async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails()
                    .Where(predicate)
                    .SingleOrDefaultAsync(GetCancellationToken(cancellationToken))
                : await DbSet
                    .Where(predicate)
                    .SingleOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        public override IQueryable<TEntity> WithDetails()
        {
            return base.WithDetails();
        }

        public override IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = GetQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }
        #endregion

        #region 增
        public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            return savedEntity;
        }
        #endregion

        #region 改
        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Attach(entity);

            var updatedEntity = DbContext.Update(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }

            return updatedEntity;
        }
        #endregion

        #region 删
        public override async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }

        public override async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities = await GetQueryable()
                .Where(predicate)
                .ToListAsync(GetCancellationToken(cancellationToken));

            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }

            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
        }
        #endregion
    }


    public abstract class EfRepository<TEntity, TKey, TDbContext> : EfRepository<TEntity, TDbContext>, IEfRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TDbContext : EfDbContext<TDbContext>
    {
        public EfRepository(TDbContext context) : base(context)
        {
        }

        #region 查
        public virtual async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, includeDetails, GetCancellationToken(cancellationToken));

            if (entity == null)
            {
                throw new Exception($"Id为{id}的{typeof(TEntity)}实体不存在");
            }

            return entity;
        }

        public virtual async Task<TEntity> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await WithDetails().FirstOrDefaultAsync(e => e.Id.Equals(id), GetCancellationToken(cancellationToken))
                : await DbSet.FindAsync(new object[] { id }, GetCancellationToken(cancellationToken));
        }
        #endregion

        #region 删
        public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, cancellationToken: cancellationToken);
            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, autoSave, cancellationToken);
        }
        #endregion
    }

}
