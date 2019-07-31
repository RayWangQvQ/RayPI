using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//
//
using RayPI.ConfigService;
using RayPI.AuthService;
using RayPI.SwaggerService;
using RayPI.CorsService;
using RayPI.Filters;


namespace RayPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            _env = env;

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _config = builder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//设置时间格式
                });

            //注册配置管理服务
            services.AddConfigService(_config);

            services.AddSwaggerService();

            services.AddAuthService(_config);

            #region 注册CORS服务
            services.AddCorsService();
            #endregion

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionFilter>();//自定义异常处理中间件

            app.UseSwaggerService();

            app.UseAuthService();

            app.UseMvc();

            app.UseStaticFiles();//用于访问wwwroot下的文件 
        }
    }
}
