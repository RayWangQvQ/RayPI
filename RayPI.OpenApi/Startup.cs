//微软包
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//三方包
using Newtonsoft.Json;
//本地项目包
using RayPI.Business.Di;
using RayPI.Repository.EFRepository.Di;
using RayPI.Infrastructure.Auth.Di;
using RayPI.Infrastructure.Auth.Operate;
using RayPI.Infrastructure.Config.Di;
using RayPI.Infrastructure.Cors.Di;
using RayPI.Infrastructure.Swagger.Di;
using RayPI.Infrastructure.Treasury.Di;
using RayPI.Infrastructure.Auth.Jwt;
using RayPI.Infrastructure.Config;
using RayPI.Infrastructure.Config.FrameConfigModel;
using RayPI.Infrastructure.RayException.Di;
using RayPI.OpenApi.Filters;

namespace RayPI.OpenApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //注册MVC
            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(WebApiResultFilterAttribute));
                    options.RespectBrowserAcceptHeader = true;
                })
                .AddJsonOptions(options =>
                {
                    //options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//设置时间格式
                });

            //注册配置管理服务
            services.AddConfigService(_env.ContentRootPath);
            AllConfigModel allConfig = services.GetSingletonInstanceOrNull<AllConfigModel>();

            //注册Swagger
            services.AddSwaggerService();

            //注册授权认证
            JwtAuthConfigModel jwtConfig = allConfig.JwtAuthConfigModel;
            var jwtOption = new JwtOption//todo:使用AutoMapper替换
            {
                WebExp = jwtConfig.WebExp,
                AppExp = jwtConfig.AppExp,
                MiniProgramExp = jwtConfig.MiniProgramExp,
                OtherExp = jwtConfig.OtherExp,
                SecurityKey = jwtConfig.SecurityKey
            };
            services.AddAuthService(jwtOption);

            //注册Cors跨域
            services.AddCorsService();

            //注册http上下文访问器
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            //注册仓储
            string connStr = allConfig.ConnectionStringsModel.SqlServerDatabase;
            services.AddRepository(connStr);

            //注册业务逻辑
            services.AddBusiness();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseExceptionService();//自定义异常处理中间件

            app.UseAuthService();

            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerService();

            app.UseStaticFiles();//用于访问wwwroot下的文件
        }

    }
}
