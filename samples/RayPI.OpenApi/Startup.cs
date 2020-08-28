using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ray.Infrastructure.DI;
using RayPI.Infrastructure.Config.Di;
using RayPI.Infrastructure.Cors.Di;
using RayPI.Infrastructure.Auth.Jwt;
using RayPI.Infrastructure.Config;
using RayPI.Infrastructure.Config.ConfigModel;
using RayPI.OpenApi.Filters;
using RayPI.Infrastructure.Auth;
using RayPI.Infrastructure.Swagger.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;
using RayPI.Repository.EFRepository;
using RayPI.Application;

namespace RayPI.OpenApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this._configuration = configuration;
            _env = env;
        }

        /// <summary>
        /// 注册服务到[依赖注入容器]
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //注册控制器
            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(WebApiResultFilterAttribute));
                    options.RespectBrowserAcceptHeader = true;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//设置时间格式
                });

            //注册配置管理服务
            services.AddConfigService(_env.ContentRootPath);
            AllConfigModel allConfig = services.GetImplementationInstanceOrNull<AllConfigModel>();

            //注册Swagger
            services.AddSwaggerService();

            //注册授权认证
            JwtAuthConfigModel jwtConfig = allConfig.JwtAuthConfigModel;
            var jwtOption = new JwtOption//todo:使用AutoMapper替换
            {
                Issuer = jwtConfig.Issuer,
                Audience = jwtConfig.Audience,
                WebExp = jwtConfig.WebExp,
                AppExp = jwtConfig.AppExp,
                MiniProgramExp = jwtConfig.MiniProgramExp,
                OtherExp = jwtConfig.OtherExp,
                SecurityKey = jwtConfig.SecurityKey
            };
            services.AddSingleton(jwtOption);
            services.AddRayAuthService(jwtOption);

            //services.AddSecurityService();

            //注册Cors跨域
            services.AddCorsService();

            //注册http上下文访问器
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            //注册业务逻辑
            services.AddMyAppServices();
            services.AddMyRepository(_configuration);
        }

        /*
        /// <summary>
        /// Autofac注册
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddMyRepository();
        }
        */

        /// <summary>
        /// 配置管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //可以拿到根容器存储下，方便以后调用
            RayContainer.ServiceProviderRoot = app.ApplicationServices;
            LogServices(RayContainer.ServiceProviderRoot);

            app.UseMyRepository();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseExceptionService();//自定义异常处理中间件

            app.UseHttpsRedirection();

            app.UseStaticFiles();//用于访问wwwroot下的文件
            app.UseRouting();

            app.UseCors();

            app.UseAuthService();
            //app.UseSecurityService();

            app.UseSwaggerService();

            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static async Task LogServices(IServiceProvider sp)
        {
            string servicesJson = MsDiHelper.SerializeServiceDescriptors(sp, o => { o.IsSerializeImplementationInstance = false; });
            await File.WriteAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "services.txt"), servicesJson);
        }
    }
}
