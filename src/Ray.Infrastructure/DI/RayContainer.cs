using System;

namespace Ray.Infrastructure.DI
{
    public static class RayContainer
    {
        /// <summary>
        /// MsDi根容器(引擎域)
        /// </summary>
        public static IServiceProvider ServiceProviderRoot { get; set; }
    }
}
