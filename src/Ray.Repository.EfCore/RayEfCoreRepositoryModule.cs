using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Auditing;
using Ray.Infrastructure.Auditing.PropertySetter;
using Ray.Infrastructure.Security.Claims;
using Ray.Infrastructure.Security.User;

namespace Ray.Repository.EfCore
{
    public static class RayEfCoreRepositoryModule
    {
        public static IServiceCollection AddRayEfRepository<TDbContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
            where TDbContext : EfDbContext
        {
            //注册审计模块
            services.AddRayAuditing();

            //DbContext
            services.AddDbContext<TDbContext>(optionsAction);
            services.AddScoped<DbContext, TDbContext>();

            //UnitOfWork
            services.AddScoped<IUnitOfWork, TDbContext>();

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
