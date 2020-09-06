using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ray.EventBus.Abstractions;
using RayPI.Application.IntegrationEvents.Events;

namespace RayPI.Application.IntegrationEvents.EventHandlers
{
    public class ArticleAddedIntegrationEventHandler : IIntegrationEventHandler<ArticleAddedIntegrationEvent>
    {
        private readonly ILogger<ArticleAddedIntegrationEventHandler> _logger;

        public ArticleAddedIntegrationEventHandler(ILogger<ArticleAddedIntegrationEventHandler> logger)
        {
            this._logger = logger;
        }

        public async Task Handle(ArticleAddedIntegrationEvent @event)
        {
            await Task.Run(() => _logger.LogInformation("进入综合事件处理器，接受到了综合事件：{0}", @event.GetType().Name));
        }
    }
}
