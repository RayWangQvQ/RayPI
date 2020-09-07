using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.EventBus.SubscriptionsManagers
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    public class SubscriptionInfo
    {
        /// <summary>
        /// 事件类是否为动态类型
        /// </summary>
        public bool IsDynamic { get; }

        /// <summary>
        /// 处理器类型
        /// </summary>
        public Type HandlerType { get; }

        private SubscriptionInfo(bool isDynamic, Type handlerType)
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Dynamic(Type handlerType)
        {
            return new SubscriptionInfo(true, handlerType);
        }
        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(false, handlerType);
        }
    }
}
