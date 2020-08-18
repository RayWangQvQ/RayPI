using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ray.Infrastructure.Extensions.Json;

namespace RayPI.OpenApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        public static IServiceProvider ServiceProvider;

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            IHost host = hostBuilder.Build();

            /*
             * Build顺序：
             * BuildHostConfiguration();
             * CreateHostingEnvironment();
             * CreateHostBuilderContext();
             * BuildAppConfiguration();
             * CreateServiceProvider();
             */

            ServiceProvider = host.Services;
            //打印容器
            LogContainer(host);

            host.Run();
        }
        /// <summary>
        /// 生成IHost构建器
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

            return hostBuilder;
        }


        private static void LogContainer(IHost host)
        {
            string builderJson = host.Services
                .AsJsonStr(option =>
                {
                    //option.EnumToString = true;
                    option.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    };
                    option.FilterProps = new FilterPropsOption
                    {
                        FilterEnum = FilterEnum.Ignore,
                        Props = new[]
                        {
                            "UsePollingFileWatcher", "Action", "Method", "Assembly","CustomAttributes","assemblies"
                        }
                    };
                }).AsFormatJsonStr();
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "container.txt"), builderJson);
        }
    }
}
