//微软包
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RayPI.Infrastructure.Config.Di
{
    public static class ConfigDiExtension
    {
        public static IServiceCollection AddConfigService(this IServiceCollection services, string basePath)
        {
            //1.创建IConfiguration构建器
            IConfigurationBuilder builder = new ConfigurationBuilder();

            //2.设置配置源地址
            builder.SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.Development.json", true, true);

            //3.构建IConfiguration
            IConfigurationRoot config = builder.Build();

            //4.注册到容器
            services.AddSingleton(config);
            AddAssemblyConfigModel(services, config);

            return services;
        }

        /// <summary>
        /// 注册ConfigModel实例到容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static IServiceCollection AddAssemblyConfigModel(IServiceCollection services, IConfigurationRoot config)
        {
            var typeList = new List<Type>();//所有符合注册条件的类集合

            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            //筛选当前程序集下符合条件的类
            List<Type> types = executingAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsGenericType)//排除了泛型类
                .Where(x => x.Name.EndsWith("ConfigModel"))
                .ToList();

            typeList.AddRange(types);
            if (!typeList.Any()) return services;

            foreach (var type in typeList)
            {
                ConstructorInfo[] constructors = type.GetConstructors();
                //ConstructorInfo constructorInfo = constructors.FirstOrDefault(it => it.GetCustomAttributes(false).OfType<InjectionAttribute>().Any());
                ConstructorInfo constructorInfo = constructors.FirstOrDefault();
                constructorInfo ??= constructors.First();
                var arguments = new object[] { config };
                var obj = constructorInfo.Invoke(arguments);
                services.AddSingleton(type, obj);
            }

            return services;
        }
    }
}
