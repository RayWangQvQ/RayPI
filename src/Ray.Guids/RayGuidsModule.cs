using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ray.Guids
{
    public static class RayGuidsModule
    {
        public static IServiceCollection AddRayGuidsModule(this IServiceCollection services)
        {
            services.AddSingleton<IGuidGenerator, SimpleGuidGenerator>();
            return services;
        }
    }
}
