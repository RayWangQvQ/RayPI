using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Ray.EventBus.Abstractions;
using Ray.EventBus.SubscriptionsManagers;

namespace Ray.EventBus.RabbitMQ
{
    public static class RayRabbitMQEventBusModule
    {
        public static IServiceCollection AddRayRabbitMQEventBus(this IServiceCollection services, Action<RabbitMqOptions> options)
        {
            services.Configure<RabbitMqOptions>(options);

            return services.AddRayRabbitMQEventBus();
        }

        public static IServiceCollection AddRayRabbitMQEventBus(this IServiceCollection services)
        {
            //注册订阅管理器
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            //注册持续连接
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var options = sp.GetRequiredService<IOptionsMonitor<RabbitMqOptions>>()
                                .CurrentValue;

                var factory = new ConnectionFactory()
                {
                    HostName = options.EventBusConnection,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(options.EventBusUserName))
                {
                    factory.UserName = options.EventBusUserName;
                }

                if (!string.IsNullOrEmpty(options.EventBusPassword))
                {
                    factory.Password = options.EventBusPassword;
                }

                var retryCount = 5;
                if (options.EventBusRetryCount > 0)
                {
                    retryCount = options.EventBusRetryCount;
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            //注册事件总线
            services.AddSingleton<IEventBus, RabbitMQEventBus>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<RabbitMQEventBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new RabbitMQEventBus(rabbitMQPersistentConnection,
                    logger,
                    eventBusSubcriptionsManager,
                    sp,
                    sp.GetRequiredService<IOptionsMonitor<RabbitMqOptions>>());
            });

            return services;
        }
    }
}
