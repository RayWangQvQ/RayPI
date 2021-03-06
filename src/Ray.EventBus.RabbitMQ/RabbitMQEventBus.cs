﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Ray.EventBus.Abstractions;
using Ray.EventBus.Events;
using Ray.EventBus.Extensions;
using Ray.EventBus.SubscriptionsManagers;

namespace Ray.EventBus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        const string BROKER_NAME = "ray_event_bus";
        const string AUTOFAC_SCOPE_NAME = "ray_event_bus";

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<RabbitMQEventBus> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _retryCount;
        private IModel _consumerChannel;
        private string _queueName;

        public RabbitMQEventBus(
            IRabbitMQPersistentConnection persistentConnection,
            ILogger<RabbitMQEventBus> logger,
            IEventBusSubscriptionsManager subsManager,
            IServiceProvider serviceProvider,
            IOptionsMonitor<RabbitMqOptions> options
            )
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            this._serviceProvider = serviceProvider;
            _queueName = options.CurrentValue.SubscriptionClientName;
            _retryCount = options.CurrentValue.EventBusRetryCount;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;

            //创建消费者连接通道（会声明交换器和队列）
            _consumerChannel = CreateConsumerChannel();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            string eventName = @event.GetType().Name;

            //创建IModel
            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);
            using IModel channel = _persistentConnection.CreateModel();

            //声明交换器（direct类型）
            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);
            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            RetryPolicy policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) =>
                    {
                        _logger.LogWarning(ex,
                            "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})",
                            @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                    });
            policy.Execute(() =>
            {
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; //设置消息持久化

                _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

                channel.BasicPublish(
                    exchange: BROKER_NAME,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            });
        }

        #region 订阅
        public void SubscribeDynamic<THandler>(string eventName)
            where THandler : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(THandler).GetGenericTypeName());

            DoInternalSubscription(eventName);

            _subsManager.AddDynamicSubscription<THandler>(eventName);

            StartBasicConsume();
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

            _subsManager.AddSubscription<T, TH>();
            StartBasicConsume();
        }

        /// <summary>
        /// 订阅
        /// （主要负责绑定交换器和队列）
        /// </summary>
        /// <param name="eventName"></param>
        private void DoInternalSubscription(string eventName)
        {
            //如果订阅管理中已存在，说明已经绑定过该eventName，直接返回
            if (_subsManager.HasSubscriptionsForEvent(eventName)) return;

            //尝试连接
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            //绑定交换器与队列
            using (IModel channel = _persistentConnection.CreateModel())
            {
                channel.QueueBind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);
            }
        }

        /// <summary>
        /// 设置并开启消费
        /// </summary>
        private void StartBasicConsume()
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,//acknowledge，是否自动确认为已消费。这里设置为false，选择消费后手动回调告诉RabbitMQ已消费
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        /// <summary>
        /// 每次接受到消息触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "----- ERROR Processing message \"{Message}\"", message);
            }

            // Even on exception we take the message off the queue.
            // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
            // For more information see: https://www.rabbitmq.com/dlx.html
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        /// <summary>
        /// 处理事件
        /// （分发到具体的handler处理器）
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                IEnumerable<SubscriptionInfo> subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    object handler = _serviceProvider.GetService(subscription.HandlerType);
                    if (handler == null) continue;

                    if (subscription.IsDynamic)
                    {
                        if (!(handler is IDynamicIntegrationEventHandler dynamicHandler)) continue;

                        dynamic eventData = JObject.Parse(message);
                        await Task.Yield();
                        await dynamicHandler.Handle(eventData);
                    }
                    else
                    {
                        Type eventType = _subsManager.GetEventTypeByName(eventName);
                        object integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        Type concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                        await Task.Yield();
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }
            }
            else
            {
                _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
            }
        }
        #endregion

        #region 取消订阅
        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = _subsManager.GetEventKey<T>();

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);

                if (_subsManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }
        #endregion

        /// <summary>
        /// 创建消费者连接通道
        /// </summary>
        /// <returns></returns>
        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogTrace("Creating RabbitMQ consumer channel");
            var channel = _persistentConnection.CreateModel();

            //声明交换器
            channel.ExchangeDeclare(exchange: BROKER_NAME,
                type: "direct");

            //声明队列
            channel.QueueDeclare(queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }
    }
}
