using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ray.Infrastructure.DI.Dtos
{
    public class ServiceDescriptorDto
    {
        private readonly ServiceDescriptor _serviceDescriptor;

        public ServiceDescriptorDto(ServiceDescriptor serviceDescriptor)
        {
            _serviceDescriptor = serviceDescriptor;
        }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType => _serviceDescriptor.ServiceType.FullName;

        /// <summary>
        /// 生命周期
        /// </summary>
        public string Lifetime => _serviceDescriptor.Lifetime.ToString();

        #region 服务实现描述

        /// <summary>
        /// 服务实现类型
        /// </summary>
        public string ImplementationType => _serviceDescriptor.ImplementationType?.FullName;


        /// <summary>
        /// 服务实现实例类型
        /// </summary>
        public string ImplementationInstance => _serviceDescriptor.ImplementationInstance?.GetType().FullName;
        /// <summary>
        /// 服务实现实例
        /// </summary>
        public object ImplementationInstanceObj => _serviceDescriptor.ImplementationInstance;


        /// <summary>
        /// 服务实现的工厂方法
        /// </summary>
        public string ImplementationFactory => _serviceDescriptor.ImplementationFactory?.ToString();
        #endregion
    }
}
