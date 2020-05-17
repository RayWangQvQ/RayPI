using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Extensions;

namespace Ray.Infrastructure.EFRepository.Behaviors
{
    public class TransactionBehavior<TEFContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TEFContext : EFContext
    {
        ILogger _logger;
        TEFContext _efContext;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="efContext"></param>
        /// <param name="logger"></param>
        public TransactionBehavior(TEFContext efContext, ILogger logger)
        {
            _efContext = efContext ?? throw new ArgumentNullException(nameof(efContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            string typeName = request.GetGenericTypeName();

            try
            {
                if (_efContext.HasActiveTransaction) return await next();

                var strategy = _efContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;
                    using (var transaction = await _efContext.BeginTransactionAsync())
                    using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- 开始事务 {TransactionId} ({@Command})", transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("----- 提交事务 {TransactionId} {CommandName}", transaction.TransactionId, typeName);

                        await _efContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理事务出错 {CommandName} ({@Command})", typeName, request);

                throw;
            }
        }
    }
}
