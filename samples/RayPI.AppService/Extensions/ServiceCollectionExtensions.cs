using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RayPI.Infrastructure.Treasury.Di;

namespace RayPI.AppService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyAppServices(this IServiceCollection services)
        {
            Assembly appServiceAssembly = Assembly.GetExecutingAssembly();
            services.AddAssemblyServices(appServiceAssembly, x => x.Name.EndsWith("Service"));
            services.AddMediatR(appServiceAssembly);
            return services;
        }
    }
}
