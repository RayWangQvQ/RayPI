using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ray.Infrastructure.ObjectMapping
{
    public static class RayMapperModule
    {
        public static IServiceCollection AddRayMapper(this IServiceCollection services)
        {
            services.AddSingleton<IRayMapper, DefaultRayMapper>();
            services.AddSingleton<IAutoRayMapper, NotImplementedAutoObjectMapper>();

            return services;
        }
    }
}
