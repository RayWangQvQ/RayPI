using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ray.Application.IAppServices;
using Ray.Infrastructure.ObjectMapping.AutoMapper;

namespace RayPI.Application
{
    public static class AppServiceModule
    {
        /// <summary>
        /// 注册AppService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyAppServices(this IServiceCollection services)
        {
            Assembly appServiceAssembly = Assembly.GetExecutingAssembly();

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

            return services;
        }
    }
}
