using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ray.EventBus.Events;

namespace RayPI.Application.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishEvent(IntegrationEvent evt);
    }
}
