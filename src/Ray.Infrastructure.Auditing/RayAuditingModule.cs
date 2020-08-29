using Microsoft.Extensions.DependencyInjection;
using Ray.Infrastructure.Auditing.PropertySetter;
using Ray.Infrastructure.Security;

namespace Ray.Infrastructure.Auditing
{
    public static class RayAuditingModule
    {
        public static IServiceCollection AddRayAuditing(this IServiceCollection services)
        {
            services.AddRaySecurity();

            services.AddTransient<IAuditPropertySetter, AuditPropertySetter>();

            return services;
        }
    }
}
