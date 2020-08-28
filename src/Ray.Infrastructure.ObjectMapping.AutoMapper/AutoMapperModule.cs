using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ray.Infrastructure.ObjectMapping.AutoMapper
{
    public static class AutoMapperModule
    {
        public static IServiceCollection AddRayAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddRayMapper();

            services.Replace(
                ServiceDescriptor.Transient<IAutoRayMapper, AutoMapperAutoRayMapper>()
            );

            List<Type> profileTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var profiles = assembly.GetTypes()
                    .Where(x => typeof(Profile).IsAssignableFrom(x));
                profileTypes.AddRange(profiles);
            }

            // 添加映射规则
            if (profileTypes.Any())
                services.AddAutoMapper(profileTypes.ToArray());

            return services;
        }
    }
}
