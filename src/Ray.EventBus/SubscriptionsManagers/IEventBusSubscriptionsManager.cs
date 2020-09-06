using System;
using System.Collections.Generic;
using System.Text;
using Ray.EventBus.Abstractions;
using Ray.EventBus.Events;

namespace Ray.EventBus.SubscriptionsManagers
{
    /// <summary>
    /// 事件总线订阅管理
    /// </summary>
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        #region 添加订阅
        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicIntegrationEventHandler;

        void AddSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;
        #endregion

        #region 移除订阅
        void RemoveSubscription<T, TH>()
             where TH : IIntegrationEventHandler<T>
             where T : IntegrationEvent;

        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
        #endregion

        #region 获取订阅信息
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;

        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        #endregion

        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        string GetEventKey<T>();
    }
}
