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
            //注册审计属性赋值器
            services.AddTransient<IAuditPropertySetter, AuditPropertySetter>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<ICurrentPrincipalAccessor, ThreadCurrentPrincipalAccessor>();

            #region 注册数据库上下文,需要传入数据库配置(数据库类型,链接字符串等)
            services.AddDbContext<MyDbContext>((serviceProvider, optionAction) =>
            {
                var dbOption = serviceProvider.GetRequiredService<IOptionsSnapshot<DbOption>>();
                optionAction.UseSqlServer(dbOption.Value.ConnStr);
            });
            services.AddScoped(x => new MyDbContext(x.GetService<DbContextOptions>())
            {
                AuditPropertySetter = x.GetService<IAuditPropertySetter>()
            });
            //todo:这里为了实现属性注入写的太恶心了，待重构
            #endregion

            #region 注册仓储
            //注册实体仓储:
            services.AddAssemblyServices(Assembly.GetExecutingAssembly(), x => x.Name.EndsWith("Repository"));
            //注册泛型仓储:
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            #endregion

            return services;
        }
    }
}
