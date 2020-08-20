using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ray.Domain.Repositories;
using Ray.Infrastructure.DI;
using Ray.Infrastructure.Repository.EfCore;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Config.Options;
using RayPI.Repository.EFRepository.Repository;

namespace RayPI.Repository.EFRepository
{
    public static class RepositoryModule
    {
        /// <summary>
        /// 向容器注册仓储相关的服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOption>(configuration.GetSection("Db"));
            services.AddRayEfRepository<MyDbContext>((serviceProvider, optionAction) =>
            {
                var dbOption = serviceProvider.GetRequiredService<IOptionsSnapshot<DbOption>>();
                optionAction.UseSqlServer(dbOption.Value.ConnStr);
            });

            //泛型仓储
            services.AddTransient(typeof(IMyBaseRepository<>), typeof(MyBaseRepository<>));

            return services;
        }

        public static void AddMyRepository(this ContainerBuilder builder)
        {
            //注册泛型仓储:
            builder.RegisterGeneric(typeof(MyBaseRepository<>))
                .As(typeof(IMyBaseRepository<>))
                .As(typeof(IRepositoryBase<,>));//Autofac允许泛型属性不一样
        }

        public static void UseMyRepository(this IApplicationBuilder app)
        {
            //初始化数据库（如果数据库不存在，则创建一个）
            //using var scope = app.ApplicationServices.CreateScope();
            using var scope = RayContainer.ServiceProviderRoot.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MyDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
