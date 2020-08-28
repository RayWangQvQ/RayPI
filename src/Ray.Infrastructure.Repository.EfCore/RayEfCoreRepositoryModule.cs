using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Auditing.PropertySetter;
using Ray.Infrastructure.Security.Claims;
using Ray.Infrastructure.Security.User;

namespace Ray.Infrastructure.Repository.EfCore
{
    public static class RayEfCoreRepositoryModule
    {
        public static IServiceCollection AddRayEfRepository<TDbContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
            where TDbContext : EfDbContext
        {
            //DbContext
            services.AddDbContext<TDbContext>(optionsAction);
            services.AddScoped<DbContext, TDbContext>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, TDbContext>();

            //注册审计属性相关服务：
            services.AddScoped<IAuditPropertySetter, AuditPropertySetter>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<ICurrentPrincipalAccessor, ThreadCurrentPrincipalAccessor>();

            //注册泛型仓储:
            services.AddTransient(typeof(IBaseRepository<,>), typeof(EfRepository<,>));

            //扫描注册仓储:
            services.Scan(scan => scan
                .FromAssemblies(typeof(TDbContext).Assembly)
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            return services;
        }
    }
}
