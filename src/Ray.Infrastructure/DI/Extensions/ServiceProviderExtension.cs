using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Ray.Infrastructure.DI.Dtos;
using Ray.Infrastructure.Extensions.Json;

namespace System
{
    public static class ServiceProviderExtension
    {
        /// <summary>
        /// 获取容器引擎对象
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>ServiceProviderEngine对象</returns>
        public static object GetEngine(this IServiceProvider serviceProvider)
        {
            //因为容器、引擎、引擎域都实现了容器接口（IServiceProvider），所以要分情况
            if (serviceProvider is ServiceProvider)//根容器
            {
                return serviceProvider.GetFieldValue("_engine");
            }
            if (serviceProvider.GetType().Name.EndsWith("ServiceProviderEngine"))//引擎
            {
                return serviceProvider;
            }
            if (serviceProvider.GetType().Name.Contains("ServiceProviderEngineScope"))//引擎域
            {
                return serviceProvider.GetPropertyValue("Engine");
            }
            return null;
        }

        /// <summary>
        /// 获取根域对象
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>代表根域（根IServiceScope）的ServiceProviderEngineScope对象</returns>
        public static object GetRootScope(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetEngine()
                .GetPropertyValue("RootScope");
            /**
             * 引擎对象是唯一的，且引擎对象的RootScope属性指向根域（根引擎域）
             * ServiceProviderEngineScope对象为internal访问权限，所以返回object
             */
        }

        /// <summary>
        /// 获取容器中的服务描述池
        /// (序列化时建议忽略部分过长的属性："UsePollingFileWatcher", "Action", "Method", "Assembly" )
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IEnumerable<ServiceDescriptor> GetServiceDescriptorsFromScope(this IServiceProvider serviceProvider)
        {
            return serviceProvider
                    .GetEngine()//拿到引擎对象
                    .GetPropertyValue("CallSiteFactory")//拿到引擎的CallSiteFactory属性（CallSiteFactory对象）
                    .GetFieldValue("_descriptors")//CallSiteFactory对象内的_descriptors字段即是服务描述集合
                as IEnumerable<ServiceDescriptor>;
        }

        /// <summary>
        /// 获取容器内的实例池中已持久化的实例集合
        /// </summary>
        /// <param name="serviceProvider">容器</param>
        /// <returns></returns>
        public static Dictionary<ServiceCacheKeyDto, object> GetResolvedServicesFromScope(this IServiceProvider serviceProvider)
        {
            Dictionary<ServiceCacheKeyDto, object> result = new Dictionary<ServiceCacheKeyDto, object>();

            object obj = serviceProvider
                .GetRequiredService<IServiceProvider>() //引擎域
                .GetPropertyValue("ResolvedServices"); //返回Dictionary<Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceCacheKey,object>的装箱后的object，注意ServiceCacheKey为internal struct

            List<ServiceCacheKeyDto> keys = obj.GetPropertyValue("Keys")
                .AsJsonStr(false)
                .JsonDeserialize<IEnumerable<ServiceCacheKeyDto>>(false)
                .ToList();

            List<object> values = ((IEnumerable<object>)obj.GetPropertyValue("Values"))
                .ToList();


            for (int i = 0; i < keys.Count(); i++)
            {
                result.Add(keys[i], values[i]);
            }

            return result;
        }

        /// <summary>
        /// 获取容器内的可释放实例池中的实例集合
        /// </summary>
        /// <param name="serviceProvider">容器</param>
        /// <returns></returns>
        public static IEnumerable<object> GetDisposablesFromScope(this IServiceProvider serviceProvider)
        {
            var objList = serviceProvider
                    .GetRequiredService<IServiceProvider>()//引擎域
                    .GetFieldValue("_disposables")
                as IEnumerable<object>;

            return objList ?? new List<object>();
        }

    }
}
