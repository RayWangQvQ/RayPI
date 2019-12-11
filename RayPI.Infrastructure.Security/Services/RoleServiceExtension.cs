using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using RayPI.Infrastructure.Security.Models;

namespace RayPI.Infrastructure.Security.Services
{
    /// <summary>
    /// 角色授权服务
    /// </summary>
    public static class RoleServiceExtension
    {
        /// <summary>
        /// 角色授权服务
        /// </summary>
        /// <typeparam name="TAuthorizationRequirement">自定义验证的标识</typeparam>
        /// <param name="services">服务上下文</param>
        /// <param name="authModel">验证授权配置</param>
        /// <param name="name">定义策略名称</param>
        /// <returns></returns>
        public static IServiceCollection AddRoleService(
            this IServiceCollection services,
            AuthConfigModel authModel
            )
        {
            AuthConfig.Init(authModel);

            // 定义如何生成用户的 Token
            var tokenValidationParameters = AuthConfig.GetTokenValidationParameters();


            // 导入角色身份认证策略
            // 导入角色身份认证策略
            services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {

                options.TokenValidationParameters = tokenValidationParameters;
                options.SaveToken = true;
            });

            return services;
        }
    }
}
