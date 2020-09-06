using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ray.EventBus.Abstractions;
using Ray.EventBus.Events;

namespace RayPI.Application.IntegrationEvents
{
    public class IntegrationEventService : IIntegrationEventService
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
    }
}
