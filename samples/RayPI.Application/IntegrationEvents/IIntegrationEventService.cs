using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ray.EventBus.Events;
using RayPI.Application.IntegrationEvents.Events;

namespace RayPI.Application.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishEvent(IntegrationEvent evt);

        void ArticleAdded(ArticleAddedIntegrationEvent @event);
    }
}
