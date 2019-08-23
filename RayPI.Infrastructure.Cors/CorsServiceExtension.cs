using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.CorsService
{
    public static class CorsServiceExtension
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("Any", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });

                c.AddPolicy("Limit", policy =>
                {
                    policy
                    .WithOrigins("localhost:8083")
                    .WithMethods("get", "post", "put", "delete")
                    //.WithHeaders("Authorization");
                    .AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
