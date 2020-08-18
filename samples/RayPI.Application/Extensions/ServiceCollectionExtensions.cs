using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ray.Application.IAppServices;
using RayPI.Infrastructure.Treasury.Di;

namespace RayPI.AppService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyAppServices(this IServiceCollection services)
        {
            Assembly appServiceAssembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(appServiceAssembly);
            services.Scan(scan => scan
                .FromAssemblies(appServiceAssembly)
                .AddClasses(classes => classes.AssignableTo<IAppService>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }
    }
}
