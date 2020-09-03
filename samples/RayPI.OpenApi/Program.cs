using System;
using System.IO;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ray.Infrastructure.DI;
using Ray.Infrastructure.Extensions.Json;

namespace RayPI.OpenApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        public static IServiceProvider ServiceProviderRoot;

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

            ServiceProviderRoot = host.Services;
            //打印容器
            LogContainerAsync();

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
            //hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //引入Autofac

            return hostBuilder;
        }


        private static async void LogContainerAsync()
        {
            var content = await Task.Run<string>(() =>
             {
                 var jsonStr = MsDiHelper.SerializeServiceDescriptors(ServiceProviderRoot, o => { o.IsSerializeImplementationInstance = false; });
                 return jsonStr;
             });
            await File.WriteAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "container.txt"), content);
        }
    }
}
