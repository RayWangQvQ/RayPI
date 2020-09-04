using Microsoft.Extensions.DependencyInjection;
using Ray.Security.Claims;
using Ray.Security.User;

namespace Ray.Security
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
