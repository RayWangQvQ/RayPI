using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Ray.EventBus.Abstractions;
using Ray.EventBus.Events;
using RayPI.Application.IntegrationEvents.Events;

namespace RayPI.Application.IntegrationEvents
{
    public class IntegrationEventService : IIntegrationEventService, ICapSubscribe
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<IntegrationEventService> _logger;

        public IntegrationEventService(IEventBus eventBus,
            ILogger<IntegrationEventService> logger)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishEvent(IntegrationEvent evt)
        {
            _logger.LogInformation("----- Publishing integration event: {EventTypeName}", evt.GetType().Name);

            try
            {
                _eventBus.Publish(evt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR publishing integration event: {EventTypeName}", evt.GetType().Name);
            }
        }

        [CapSubscribe("ArticleAdded")]
        public void ArticleAdded(ArticleAddedIntegrationEvent @event)
        {
            _logger.LogInformation("进入综合事件处理器，接受到了综合事件：{0}", @event.GetType().Name);
        }
    }
}
