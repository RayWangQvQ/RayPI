using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Ray.Guids;

namespace Ray.Application
{
    public static class RayApplicationModule
    {
        public static IServiceCollection AddRayApplicationModule(this IServiceCollection services)
        {
            services.AddRayGuidsModule();

            return services;
        }
    }
}
