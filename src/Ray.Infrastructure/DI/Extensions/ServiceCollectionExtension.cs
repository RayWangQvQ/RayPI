using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 获取单例注册对象
        /// </summary>
        public static T GetImplementationInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T)services.FirstOrDefault(x => x.ServiceType == typeof(T))?.ImplementationInstance;
        }

        /// <summary>
        /// 获取单例注册对象
        /// </summary>
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            return (IConfiguration)services.FirstOrDefault(x =>
            x.ServiceType == typeof(IConfiguration)
            && x.Lifetime == ServiceLifetime.Singleton
            && x.ImplementationInstance != null)
                ?.ImplementationInstance;
        }

        /// <summary>
        /// 扫描程序集注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddAssemblyServices(this IServiceCollection services, Assembly assembly, Func<Type, bool> filter, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            //筛选当前程序集下符合注册条件的类
            var implTypeList = assembly.GetTypes()
                .Where(t => t.IsClass
                && !t.IsAbstract
                && t.IsPublic
                && !t.IsGenericType)
                .Where(filter)
                .ToList();
            if (implTypeList == null | !implTypeList.Any()) return services;

            //实现类与其接口配对
            var implAndServiceTypeDic = new Dictionary<Type, Type[]>();
            foreach (var implType in implTypeList)
            {
                Type[] interfaces = implType.GetInterfaces();
                implAndServiceTypeDic.Add(implType, interfaces);
            }

            //循环实现类
            foreach (var instanceType in implAndServiceTypeDic.Keys)
            {
                Type[] interfaceTypes = implAndServiceTypeDic[instanceType];
                if (interfaceTypes == null | interfaceTypes?.Count() == 0)//如果该类没有实现接口，则直接以其本身类型注册
                {
                    services.Add(new ServiceDescriptor(serviceType: instanceType, implementationType: instanceType, lifetime: serviceLifetime));
                }
                else//如果该类有实现接口，则循环其实现的接口，一一配对注册
                {
                    AddWithMultiInterfaces(instanceType, interfaceTypes);
                }
            }
            return services;

            //一个实现类实现了多个接口
            void AddWithMultiInterfaces(Type instanceType, Type[] interfaceTypes)
            {
                foreach (var interfaceType in interfaceTypes)
                {
                    services.Add(new ServiceDescriptor(serviceType: interfaceType, implementationType: instanceType, lifetime: serviceLifetime));
                }
            }
        }
    }
}
