using System;
using Autofac.Extensions.DependencyInjection;
using Autofac;

namespace Ray.Infrastructure.DI
{
    public static class RayContainer
    {
        private static ILifetimeScope _lifetimeScope;

        /// <summary>
        /// MsDi根容器(引擎域)
        /// </summary>
        public static IServiceProvider ServiceProviderRoot { get; set; }

        /// <summary>
        /// Autofac根域
        /// </summary>
        public static Autofac.ILifetimeScope AutofacRootScope => _lifetimeScope ?? ServiceProviderRoot.GetAutofacRoot();

        /// <summary>
        /// 初始化容器
        /// (.NET Framework框架使用)
        /// </summary>
        /// <param name="func">委托</param>
        /// <returns></returns>
        public static void Init(Action<Autofac.ContainerBuilder> func = null)
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new Autofac.ContainerBuilder();

            //注册组件
            func?.Invoke(builder);

            //利用构建器创建容器
            _lifetimeScope = builder.Build();
        }
    }
}
