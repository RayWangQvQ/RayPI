using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using RayPI.Application.IntegrationEvents.Events;

namespace RayPI.Application.IntegrationEvents.EventHandlers
{
    public class ArticleAddedIntegrationEventHandler : ICapSubscribe
    {
        private readonly ILogger<ArticleAddedIntegrationEventHandler> _logger;

        public ArticleAddedIntegrationEventHandler(ILogger<ArticleAddedIntegrationEventHandler> logger)
        {
            this._logger = logger;
        }

        [CapSubscribe("ArticleAdded")]
        public async Task Handle(ArticleAddedIntegrationEvent @event)
        {
            await Task.Run(() => _logger.LogInformation("进入综合事件处理器，接受到了综合事件：{0}", @event.GetType().Name));
        }
    }
}
