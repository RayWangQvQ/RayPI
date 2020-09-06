using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ray.EventBus.Events;

namespace Ray.EventBus.Abstractions
{
    /// <summary>
    /// 综合事件处理器
    /// </summary>
    /// <typeparam name="TIntegrationEvent"></typeparam>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    /// <summary>
    /// 综合事件处理器
    /// </summary>
    public interface IIntegrationEventHandler
    {
    }
}
