using System.Linq;
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
            return (IConfiguration)services.FirstOrDefault(x => x.ServiceType == typeof(IConfiguration))?.ImplementationInstance;
        }
    }
}
