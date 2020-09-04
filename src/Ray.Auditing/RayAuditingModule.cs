using Microsoft.Extensions.DependencyInjection;
using Ray.Auditing.PropertySetter;
using Ray.Security;

namespace Ray.Auditing
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
