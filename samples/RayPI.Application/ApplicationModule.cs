using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ray.Application.IAppServices;
using Ray.EventBus.Abstractions;
using Ray.EventBus.RabbitMQ;
using Ray.ObjectMap.AutoMapper;
using RayPI.Application.IntegrationEvents;
using RayPI.Application.IntegrationEvents.EventHandlers;
using RayPI.Application.IntegrationEvents.Events;

namespace RayPI.Application
{
    public static class ApplicationModule
    {
        /// <summary>
        /// 注册AppService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly appServiceAssembly = Assembly.GetExecutingAssembly();

            //注册事件总线
            services.AddRayRabbitMQEventBus(configuration);

            //注册AutoMapper
            services.AddRayAutoMapper(appServiceAssembly);

            //注册MediatR
            services.AddMediatR(appServiceAssembly);

            //扫描注册所有AppService
            services.Scan(scan => scan
                .FromAssemblies(appServiceAssembly)
                .AddClasses(classes => classes.AssignableTo<IAppService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            //扫描注册所有综合事件处理器
            services.Scan(scan => scan
                .FromAssemblies(appServiceAssembly)
                .AddClasses(classes => classes.AssignableTo<IIntegrationEventHandler>())
                .AsImplementedInterfaces()
                .AsSelf()
                .WithTransientLifetime());

            services.AddTransient<IIntegrationEventService, IntegrationEventService>();

            return services;
        }

        public static void ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            //订阅
            eventBus.Subscribe<ArticleAddedIntegrationEvent, ArticleAddedIntegrationEventHandler>();
        }
    }
}
