using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Extensions.Json;
using RayPI.Domain.Events;

namespace RayPI.Application.ArticleApp.DomainEventHandlers
{
    /// <summary>
    /// 文章新增事件处理器
    /// </summary>
    public class ArticleAddedDomainEventHandler : INotificationHandler<ArticleAddedDomainEvent>
    {
        private readonly ILogger<ArticleAddedDomainEventHandler> _logger;

        public ArticleAddedDomainEventHandler(ILogger<ArticleAddedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(ArticleAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            //暂时没想好要做啥，记个日志
            await Task.Run(() => _logger.LogInformation($"新增了文章：{notification.Article.AsJsonStr()}"), cancellationToken);
        }
    }
}
