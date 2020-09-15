using System.Reflection;
using DotNetCore.CAP;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ray.Application;
using Ray.Application.IAppServices;
using Ray.ObjectMap.AutoMapper;
using RayPI.Repository.EFRepository;

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
            services.AddRayApplicationModule();

            Assembly appServiceAssembly = Assembly.GetExecutingAssembly();

            //注册事件总线
            services.AddCap(options =>
            {
                //绑定数据库，会创建2张表，分别为消费消息表与发布消息表
                options.UseEntityFramework<MyDbContext>();

                //指定消息队列
                options.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });

                //options.UseDashboard();
            });

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
                .AddClasses(classes => classes.AssignableTo<ICapSubscribe>())
                .AsSelf()
                .WithTransientLifetime());

            return services;
        }
    }
}
