using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Ray.Auditing;
using Ray.Auditing.PropertySetter;
using Ray.Domain.Entities;
using Ray.Domain.Helpers;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Guids;
using Ray.Infrastructure.Helpers;

namespace Ray.Repository.EfCore
{
    /// <summary>
    /// EF的数据库上下文
    /// </summary>
    public class RayDbContext : DbContext, IUnitOfWork, ITransaction<IDbContextTransaction>
    {
        protected IMediator _mediator => _serviceProvider.GetRequiredService<IMediator>();
        ICapPublisher _capBus;
        private readonly IServiceProvider _serviceProvider;

        public IGuidGenerator GuidGenerator { get; set; }

        /// <summary>
        /// 审计属性赋值器
        /// </summary>
        public IAuditPropertySetter AuditPropertySetter => _serviceProvider.GetRequiredService<IAuditPropertySetter>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="options"></param>
        public RayDbContext(DbContextOptions options, IServiceProvider serviceProvider)
            : base(options)
        {
            GuidGenerator = SimpleGuidGenerator.Instance;
            _serviceProvider = serviceProvider;
        }


        #region 封装OnModelCreating
        /// <summary>
        /// 创建Model时执行操作
        /// （如：配置实体映射、向表初始化数据等）
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyConfigurationsFromAssembly(modelBuilder);
        }

        /// <summary>
        /// 创建Model时执行操作
        /// (默认会利用反射读取EntityTypeConfigurationAssembly程序集内所有的IEntityTypeConfiguration<TEntity>配置类)
        /// (如果不想自动化添加,可以重写后自己添加)
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void ApplyConfigurationsFromAssembly(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(RayDbContext).Assembly);
        #endregion


        #region IUnitOfWork工作单元
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            try
            {
                ApplyConcepts();

                var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        public virtual async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.PublishDomainEventsAsync(this);
            await SaveChangesAsync(cancellationToken);
            return true;
        }
        #endregion


        /// <summary>
        /// 为实体添加附加操作（逻辑删除、审计属性赋值等）
        /// </summary>
        protected virtual void ApplyConcepts()
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                ApplyConcepts(entry);
            }
        }

        /// <summary>
        /// 为实体添加附加操作
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void ApplyConcepts(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyConceptsForAddedEntity(entry);
                    break;
                case EntityState.Modified:
                    ApplyConceptsForModifiedEntity(entry);
                    break;
                case EntityState.Deleted:
                    ApplyConceptsForDeletedEntity(entry);
                    break;
            }
        }

        /// <summary>
        /// 插入实体时的附加操作
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void ApplyConceptsForAddedEntity(EntityEntry entry)
        {
            CheckAndSetId(entry);
            SetCreationAuditProperties(entry);
        }

        /// <summary>
        /// 更新实体时的附加操作
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void ApplyConceptsForModifiedEntity(EntityEntry entry)
        {
            SetModificationAuditProperties(entry);

            if (entry.Entity is ILogicDeletable && entry.Entity.As<ILogicDeletable>().IsDeleted)
            {
                SetDeletionAuditProperties(entry);
            }
        }

        /// <summary>
        /// 删除实体时的附加操作
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void ApplyConceptsForDeletedEntity(EntityEntry entry)
        {
            if (TryCancelDeletionForLogicDelete(entry))
            {
                SetDeletionAuditProperties(entry);
            }
        }

        /// <summary>
        /// 设置逻辑删除
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        protected virtual bool TryCancelDeletionForLogicDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ILogicDeletable)) return false;

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ILogicDeletable>().IsDeleted = true;
            return true;
        }


        /// <summary>
        /// 检查并设置Id
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void CheckAndSetId(EntityEntry entry)
        {
            switch (entry.Entity)
            {
                case IEntity<Guid> entityWithGuidId:
                    TrySetGuidId(entry, entityWithGuidId);
                    return;
                case IEntity<long> entityWithLongId:
                    //todo
                    return;
            }
        }

        #region 设置实体Id审计等属性值
        /// <summary>
        /// 设置Guid主键
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="entity"></param>
        protected virtual void TrySetGuidId(EntityEntry entry, IEntity<Guid> entity)
        {
            if (entity.Id != default)
            {
                return;
            }

            var idProperty = entry.Property("Id").Metadata.PropertyInfo;

            //Check for DatabaseGeneratedAttribute
            var dbGeneratedAttr = ReflectionHelper
                .GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(
                    idProperty
                );

            if (dbGeneratedAttr != null && dbGeneratedAttr.DatabaseGeneratedOption != DatabaseGeneratedOption.None)
            {
                return;
            }

            EntityHelper.TrySetId(
                entity,
                () => GuidGenerator.Create(),
                false
            );
        }

        /// <summary>
        /// 设置Long主键
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="entity"></param>
        protected virtual void TrySetLongId(EntityEntry entry, IEntity<Guid> entity)
        {
            //todo
        }

        /// <summary>
        /// 设置新增相关审计属性
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void SetCreationAuditProperties(EntityEntry entry)
        {
            AuditPropertySetter?.SetCreationProperties(entry.Entity);
        }

        /// <summary>
        /// 设置编辑相关审计属性
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void SetModificationAuditProperties(EntityEntry entry)
        {
            AuditPropertySetter?.SetModificationProperties(entry.Entity);
        }

        /// <summary>
        /// 设置软删除相关属性
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void SetDeletionAuditProperties(EntityEntry entry)
        {
            AuditPropertySetter?.SetDeletionProperties(entry.Entity);
        }
        #endregion

        #region ITransaction事务
        public virtual IDbContextTransaction CurrentTransaction { get; private set; }

        public virtual bool HasActiveTransaction => CurrentTransaction != null;

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (CurrentTransaction != null) return null;
            CurrentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);//包Microsoft.EntityFrameworkCore.Relational

            return CurrentTransaction;
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
