using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RayPI.SwaggerHelp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using RayPI.AuthHelper;
using RayPI.Token;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.StaticFiles;

namespace RayPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Dictionary<string, string>>(Configuration.GetSection("Mime"));
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//设置时间格式
            });

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1.1.0",
                    Title = "Ray WebAPI",
                    Description = "框架集合",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "RayWang", Email = "2271272653@qq.com", Url = "http://www.cnblogs.com/RayWang" }
                });
                //添加注释服务
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "APIHelp.xml");
                c.IncludeXmlComments(xmlPath);
                //添加对控制器的标签(描述)
                c.DocumentFilter<SwaggerDocTag>();

                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                c.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
            });
            #endregion

            #region Token
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("System", policy => policy.RequireClaim("SystemType").Build());
                options.AddPolicy("Client", policy => policy.RequireClaim("ClientTpe").Build());
                options.AddPolicy("Admin", policy => policy.RequireClaim("AdminType").Build());
            });
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

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion

            #region TokenAuth
            app.UseMiddleware<TokenAuth>();
            #endregion

            app.UseMvc();
            
            app.UseStaticFiles();//用于访问wwwroot下的文件 
        }
    }
}
