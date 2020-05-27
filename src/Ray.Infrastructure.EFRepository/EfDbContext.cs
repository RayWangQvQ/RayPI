using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ray.Domain;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;

namespace Ray.Infrastructure.EFRepository
{
    /// <summary>
    /// EF的数据库上下文
    /// </summary>
    public abstract class EfDbContext<TDbContext> : DbContext, IUnitOfWork, ITransaction<IDbContextTransaction>
    {
        protected IMediator _mediator;
        ICapPublisher _capBus;

        /// <summary>
        /// 构造
        /// </summary>
        public EfDbContext() : base()
        {

        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="options"></param>
        public EfDbContext(DbContextOptions options)
            : base(options)
        {

        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
        /// <param name="capBus"></param>
        public EfDbContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus)
            : base(options)
        {
            _mediator = mediator;
            _capBus = capBus;
        }

        #region 封装OnModelCreating
        private static readonly MethodInfo ApplyConfigurationsFromAssemblyMethodInfo
            = typeof(TDbContext)
                .GetMethod(
                    nameof(ApplyConfigurationsFromAssembly),
                    BindingFlags.Instance | BindingFlags.NonPublic
                );
        /// <summary>
        /// 实体映射配置类所在程序集
        /// (用于OnModelCreating方法利用反射批量关联映射类)
        /// </summary>
        protected abstract Assembly EntityTypeConfigurationAssembly { get; }

        /// <summary>
        /// 创建Model时执行操作
        /// （如：配置实体映射、向表初始化数据等）
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyConfigurationsFromAssemblyMethodInfo.Invoke(this, new object[] { modelBuilder });
        }
        /// <summary>
        /// 创建Model时执行操作
        /// (默认会利用反射读取EntityTypeConfigurationAssembly程序集内所有的IEntityTypeConfiguration<TEntity>配置类)
        /// (如果不想自动化添加,可以重写后自己添加)
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void ApplyConfigurationsFromAssembly(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assembly2 = typeof(TDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly2);
        }
        #endregion

        #region 封装DbSet，使可以使用泛型读取
        /// <summary>DbSet</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        public DbSet<TAggregateRoot> RayDbSet<TAggregateRoot>() where TAggregateRoot : class
        {
            //Set方法会做GetOrAdd,所以可以利用传入泛型实体来获取任意实体的DbSet
            return this.Set<TAggregateRoot>();
        }
        #endregion

        /// <summary>Sets the modified.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="entity">The entity.</param>
        public void SetModified<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : IAggregateRoot
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>Sets the deleted.</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="entity">The entity.</param>
        public void SetDeleted<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : IAggregateRoot
        {
            this.Entry(entity).State = EntityState.Deleted;
        }

        #region IUnitOfWork工作单元
        public virtual async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await _mediator.PublishDomainEventsAsync(this);
            return true;
        }
        #endregion

        #region ITransaction事务
        public virtual IDbContextTransaction CurrentTransaction { get; private set; }

        public virtual bool HasActiveTransaction => CurrentTransaction != null;

        public virtual Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (CurrentTransaction != null) return null;
            CurrentTransaction = Database.BeginTransaction(_capBus, autoCommit: false);//包DotNetCore.CAP.MySql
            return Task.FromResult(CurrentTransaction);
        }

        public virtual async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != CurrentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (CurrentTransaction != null)
                {
                    CurrentTransaction.Dispose();
                    CurrentTransaction = null;
                }
            }
        }

        public virtual void RollbackTransaction()
        {
            try
            {
                CurrentTransaction?.Rollback();
            }
            finally
            {
                if (CurrentTransaction != null)
                {
                    CurrentTransaction.Dispose();
                    CurrentTransaction = null;
                }
            }
        }
        #endregion
    }
}
