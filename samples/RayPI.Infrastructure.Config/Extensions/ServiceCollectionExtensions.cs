using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RayPI.Infrastructure.Config.Options;

namespace RayPI.Infrastructure.Config.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyOptions(this IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();

            services.AddOptions()
                .Configure<DbOption>(configuration.GetSection("Db"));
            return services;
        }
    }
}
