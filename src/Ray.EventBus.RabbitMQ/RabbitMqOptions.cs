using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.EventBus.RabbitMQ
{
    public class RabbitMqOptions
    {
        /// <summary>
        /// 订阅端名称
        /// （将作为队列名称）
        /// </summary>
        public string SubscriptionClientName { get; set; }

        /// <summary>
        /// 连接地址
        /// </summary>
        public string EventBusConnection { get; set; }

        /// <summary>
        /// 连接用户名
        /// （为空则使用默认用户）
        /// </summary>
        public string EventBusUserName { get; set; }

        /// <summary>
        /// 连接密码
        /// （为空则使用默认密码）
        /// </summary>
        public string EventBusPassword { get; set; }

        /// <summary>
        /// 连接重试次数
        /// </summary>
        public int EventBusRetryCount { get; set; }
    }
}
