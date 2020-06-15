using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Ray.Infrastructure.Auditing;
using Ray.Infrastructure.Auditing.PropertySetter;
using Ray.Infrastructure.Security.Claims;
using Ray.Infrastructure.Security.User;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Config.Options;
using RayPI.Infrastructure.Treasury.Di;
using RayPI.Repository.EFRepository.Repository;

namespace RayPI.Repository.EFRepository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 向容器注册仓储相关的服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyRepository(this IServiceCollection services)
        {
            //推荐使用Autofac注册

            return services;
        }
    }
}
