//系统包
using System.Collections.Generic;
using System.IO;
//微软包
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
//三方包
using Swashbuckle.AspNetCore.Swagger;

namespace RayPI.Infrastructure.Swagger.Di
{
    public static class SwaggerDiExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                Name = "Authorization", //jwt默认的参数名称
                Scheme = "Authorization",
                In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
            };

            var apiInfo = new OpenApiInfo
            {
                Version = "v2.0.0",
                Title = "Ray WebAPI",
                Description = "框架集合",
                Contact = new OpenApiContact
                {
                    Name = "RayWang",
                    Email = "2271272653@qq.com",
                    Url = new System.Uri("http://www.cnblogs.com/RayWang")
                }
            };
            #region 注册Swagger服务
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", apiInfo);

                //添加注释服务
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var apiXmlPath = Path.Combine(basePath, "ApiDoc.xml");//控制器层注释
                var entityXmlPath = Path.Combine(basePath, "EntityDoc.xml");//实体注释
                c.IncludeXmlComments(apiXmlPath, true);//true表示显示控制器注释
                //c.IncludeXmlComments(entityXmlPath);

                //添加控制器注释
                //c.DocumentFilter<SwaggerDocTag>();

                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                //var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                c.AddSecurityDefinition("oauth2", securityScheme);
            });
            #endregion

            return services;
        }

        public static void UseSwaggerService(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
        }
    }
}
