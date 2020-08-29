using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Ray.Infrastructure.Security.Claims;
using Ray.Infrastructure.Security.User;

namespace Ray.Infrastructure.Security
{
    public static class RaySecurityModule
    {
        public static IServiceCollection AddRaySecurity(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<ICurrentPrincipalAccessor, ThreadCurrentPrincipalAccessor>();

            return services;
        }
    }
}
