//微软包
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//本地项目包
using RayPI.Infrastructure.Config.Model;

namespace RayPI.Infrastructure.Config.Di
{
    public static class ConfigDiExtension
    {
        public static IServiceCollection AddConfigService(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(config);
            services.AddScoped<JwtAuthConfigModel>();

            return services;
        }
    }
}
