using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ray.Infrastructure.Repository;

namespace Ray.Infrastructure.EFRepository
{
    /// <summary>
    /// EF的数据库上下文
    /// </summary>
    public class EFContext : DbContext, IUnitOfWork, ITransaction<IDbContextTransaction>
    {
        protected IMediator _mediator;
        ICapPublisher _capBus;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
        /// <param name="capBus"></param>
        public EFContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) 
            : base(options)
        {
            _mediator = mediator;
            _capBus = capBus;
        }

        #region IUnitOfWork
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await _mediator.PublishDomainEventsAsync(this);
            return true;
        }
        #endregion

        #region ITransaction
        public IDbContextTransaction CurrentTransaction { get; private set; }

        public bool HasActiveTransaction => CurrentTransaction != null;

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (CurrentTransaction != null) return null;
            CurrentTransaction = Database.BeginTransaction(_capBus, autoCommit: false);//包DotNetCore.CAP.MySql
            return Task.FromResult(CurrentTransaction);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
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

        public void RollbackTransaction()
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
