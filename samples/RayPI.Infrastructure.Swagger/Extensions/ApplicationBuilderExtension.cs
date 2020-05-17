using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace RayPI.Infrastructure.Swagger.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSwaggerService(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v3/swagger.json", "ApiHelp V3");
            });
        }
    }
}
