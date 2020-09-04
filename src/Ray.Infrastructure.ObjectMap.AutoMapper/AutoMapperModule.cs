using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ray.ObjectMap.AutoMapper
{
    public static class AutoMapperModule
    {
        public static IServiceCollection AddRayAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            //注册RayMapper
            services.AddRayMapper();

            services.Replace(
                ServiceDescriptor.Transient<IAutoObjectMapper, AutoMapperAutoObjectMapper>()
            );

            //添加AutoMapper服务
            if (assemblies.Any())
                services.AddAutoMapper(assemblies);

            return services;
        }
    }
}
