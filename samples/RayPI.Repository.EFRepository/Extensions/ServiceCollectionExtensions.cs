using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Auditing;
using Ray.Infrastructure.Auditing.PropertySetter;
using Ray.Infrastructure.Repository.EfCore;
using Ray.Infrastructure.Security.Claims;
using Ray.Infrastructure.Security.User;
using RayPI.Domain.Entity;
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
        public static IServiceCollection AddMyRepository(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.Configure<DbOption>(configuration.GetSection("Db"));

            services.AddDbContext<MyDbContext>((serviceProvider, optionAction) =>
            {
                var dbOption = serviceProvider.GetRequiredService<IOptionsSnapshot<DbOption>>();
                optionAction.UseSqlServer(dbOption.Value.ConnStr);
            });

            //注册审计属性相关服务：
            services.AddScoped<IAuditPropertySetter, AuditPropertySetter>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<ICurrentPrincipalAccessor, ThreadCurrentPrincipalAccessor>();

            #region 注册仓储
            //注册泛型仓储:
            //services.AddTransient(typeof(IRepositoryBase<,>), typeof(EfRepository<,,>));//todo
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //扫描注册仓储:
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            #endregion

            return services;
        }
    }
}
