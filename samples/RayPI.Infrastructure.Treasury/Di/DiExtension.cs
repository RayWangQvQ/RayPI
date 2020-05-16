//系统包
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//微软包
using Microsoft.Extensions.DependencyInjection;

namespace RayPI.Infrastructure.Treasury.Di
{
    public static class DiExtension
    {
        /// <summary>
        /// 反射注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddAssemblyServices(this IServiceCollection services, Assembly assembly, Func<Type, bool> filter, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var typeList = new List<Type>();//所有符合注册条件的类集合

            //筛选当前程序集下符合条件的类
            List<Type> types = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsGenericType)//排除了泛型类
                .Where(filter)
                .ToList();

            typeList.AddRange(types);
            if (!typeList.Any()) return services;

            var typeDic = new Dictionary<Type, Type[]>(); //待注册集合<class,interface>
            foreach (var type in typeList)
            {
                var interfaces = type.GetInterfaces();   //获取接口
                typeDic.Add(type, interfaces);
            }

            //循环实现类
            foreach (var instanceType in typeDic.Keys)
            {
                Type[] interfaceTypeList = typeDic[instanceType];
                if (interfaceTypeList == null | interfaceTypeList?.Count() == 0)//如果该类没有实现接口，则以其本身类型注册
                {
                    services.AddServiceWithLifeScoped(null, instanceType, serviceLifetime);
                }
                else//如果该类有实现接口，则循环其实现的接口，一一配对注册
                {
                    foreach (var interfaceType in interfaceTypeList)
                    {
                        services.AddServiceWithLifeScoped(interfaceType, instanceType, serviceLifetime);
                    }
                }
            }
            return services;
        }

        /// <summary>
        /// 暴露类型可空注册
        /// （如果暴露类型为null，则自动以其本身类型注册）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interfaceType"></param>
        /// <param name="instanceType"></param>
        /// <param name="serviceLifetime"></param>
        private static void AddServiceWithLifeScoped(this IServiceCollection services, Type interfaceType, Type instanceType, ServiceLifetime serviceLifetime)
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    if (interfaceType == null) services.AddScoped(instanceType);
                    else services.AddScoped(interfaceType, instanceType);
                    break;
                case ServiceLifetime.Singleton:
                    if (interfaceType == null) services.AddSingleton(instanceType);
                    else services.AddSingleton(interfaceType, instanceType);
                    break;
                case ServiceLifetime.Transient:
                    if (interfaceType == null) services.AddTransient(instanceType);
                    else services.AddTransient(interfaceType, instanceType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null);
            }
        }
    }
}
