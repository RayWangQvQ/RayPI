using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RayPI.Infrastructure.Security.Interface;
using RayPI.Infrastructure.Security.Models;
using RayPI.Infrastructure.Security.Services;
using System;

namespace RayPI.Infrastructure.Security
{
    public static class StartupExtension
    {
        /// <summary>
        /// 注册到容器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSecurityService(this IServiceCollection services)
        {
            var authOptions = new AuthBuilder()
                .Security("aaaafsfsfdrhdhrejtrjrt", "ASPNETCORE", "ASPNETCORE")
                .Jump("accoun/login", "account/error", false, false)
                .Time(TimeSpan.FromMinutes(20))
                .InfoScheme(new AuthenticateScheme
                {
                    TokenEbnormal = "Login authentication failed!1",
                    TokenIssued = "Login authentication failed!2",
                    NoPermissions = "Login authentication failed!3"
                }).Build();
            services.AddSingleton<IRoleEventsHandler, RoleEvents>();
            services.AddRoleService(authOptions);

            services.AddAuthorization();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Test", policy => policy.AddRequirements(new PolicyRequirement(Enums.OperateEnum.Retrieve, Enums.ResourceEnum.Student)));
            });
            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();

            return services;
        }

        /// <summary>
        /// 配置管道
        /// </summary>
        /// <param name="app"></param>
        public static void UseSecurityService(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<RoleMiddleware>();
        }
    }
}
