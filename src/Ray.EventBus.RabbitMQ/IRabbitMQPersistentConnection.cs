using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EventBus.RabbitMQ
{
    /// <summary>
    /// RabbitMQ持续连接接口
    /// （负责重试与创建AMQP M odel）
    /// </summary>
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 尝试连接
        /// </summary>
        /// <returns></returns>
        bool TryConnect();

        /// <summary>
        /// 创建RabbitMQ封装的IModel
        /// （RabbitMQ将大部分API都封装在了其中）
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
    }
}
