using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//
using RayPI.ConfigService.ConfigModel;


namespace RayPI.ConfigService
{
    public static class ConfigManagerExtension
    {
        public static IServiceCollection AddConfigService(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(config);
            services.AddScoped<JwtAuthConfigModel>();

            return services;
        }
    }
}
