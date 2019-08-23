using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using RayPI.AuthService.Enums;
using RayPI.AuthService.Jwt;

namespace RayPI.AuthService
{
    public static class AuthServiceExtension
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IJwtServicecs,JwtService>();

            #region 注册【认证】服务
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    string jwtSecretKey = config["JwtAuth:SecurityKey"] ?? "";
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "RayPI",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecretKey)),

                        /***********************************TokenValidationParameters的参数默认值***********************************/
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        // SaveSigninToken = false,
                        // ValidateActor = false,
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        // ClockSkew = TimeSpan.FromSeconds(300),// 允许的服务器时间偏移量
                        ValidateLifetime = true// 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            //Token expired
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            #endregion

            #region 注册【授权】服务
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyEnum.RequireRoleOfClient.ToString(), policy => policy.AddRequirements(new PolicyRequirement("Client")));
                options.AddPolicy(PolicyEnum.RequireRoleOfAdmin.ToString(), policy => policy.AddRequirements(new PolicyRequirement("Admin")));
                options.AddPolicy(PolicyEnum.RequireRoleOfAdminOrClient.ToString(), policy => policy.AddRequirements(new PolicyRequirement("Admin,Client")));
            });
            #endregion

            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();

            return services;
        }

        public static void UseAuthService(this IApplicationBuilder app)
        {
            //认证
            app.UseAuthentication();

            //授权
            //app.UseMiddleware<JwtAuthorizationFilter>();
        }
    }
}
