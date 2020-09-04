using System;
using System.Xml.XPath;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ray.Auditing;
using Ray.Domain.Repositories;
using Ray.Repository.EfCore.Repositories;

namespace Ray.Repository.EfCore
{
    public static class RayEfCoreRepositoryModule
    {
        public static IServiceCollection AddRayEfRepository<TAppDbContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
            where TAppDbContext : RayDbContext, IUnitOfWork
        {
            #region 依赖其他模块
            //注册审计模块
            services.AddRayAuditing();
            #endregion

            //DbContext
            services.AddDbContext<TAppDbContext>(optionsAction);
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<TAppDbContext>());

            //UnitOfWork
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TAppDbContext>());

            //注册泛型仓储:
            services.AddTransient(typeof(IBaseRepository<,>), typeof(EfRepository<,>));

            //扫描注册仓储:
            services.Scan(scan => scan
                .FromAssemblies(typeof(TAppDbContext).Assembly)
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            return services;
        }
    }
}
