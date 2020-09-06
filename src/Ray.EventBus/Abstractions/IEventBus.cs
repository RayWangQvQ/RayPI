using System;
using System.Collections.Generic;
using System.Text;
using Ray.EventBus.Events;

namespace Ray.EventBus.Abstractions
{
    /// <summary>
    /// 综合事件总线
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="event"></param>
        void Publish(IntegrationEvent @event);

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        void Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="eventName"></param>
        void SubscribeDynamic<THandler>(string eventName)
            where THandler : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<THandler>(string eventName)
            where THandler : IDynamicIntegrationEventHandler;

        void Unsubscribe<TEvent, THandler>()
            where THandler : IIntegrationEventHandler<TEvent>
            where TEvent : IntegrationEvent;
    }
}
