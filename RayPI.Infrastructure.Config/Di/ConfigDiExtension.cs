//微软包
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//本地项目包
using RayPI.Infrastructure.Config.Model;

namespace RayPI.Infrastructure.Config.Di
{
    public static class ConfigDiExtension
    {
        public static IServiceCollection AddConfigService(this IServiceCollection services, string basePath)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", true, true);
            var config = builder.Build();

            services.AddSingleton(config);

            var allConfigModel = new AllConfigModel(config);
            services.AddSingleton(allConfigModel);

            return services;
        }
    }
}
