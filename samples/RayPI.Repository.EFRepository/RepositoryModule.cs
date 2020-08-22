using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ray.Domain.Repositories;
using Ray.Infrastructure.DI;
using Ray.Infrastructure.Repository.EfCore;
using RayPI.Domain.IRepositories;
using RayPI.Infrastructure.Config.Options;
using RayPI.Repository.EFRepository.Repositories;

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
            var dbType = configuration["Db:DbType"];
            services.Configure<DbOption>(configuration.GetSection($"Db:{dbType}"));
            services.AddRayEfRepository<MyDbContext>((serviceProvider, optionAction) =>
            {
                var dbOption = serviceProvider.GetRequiredService<IOptionsSnapshot<DbOption>>();
                switch (dbType)
                {
                    case "SqlServer":
                        optionAction.UseSqlServer(dbOption.Value.ConnStr);
                        break;
                    case "Sqlite":
                        optionAction.UseSqlite(dbOption.Value.ConnStr);
                        break;
                    default:
                        break;
                }
            });

            return services;
        }

        /// <summary>
        /// 使用Autofac注册
        /// </summary>
        /// <param name="builder"></param>
        public static void AddMyRepository(this ContainerBuilder builder)
        {
            
        }

        /// <summary>
        /// 中间件相关
        /// </summary>
        /// <param name="app"></param>
        public static void UseMyRepository(this IApplicationBuilder app)
        {
            //初始化数据库（如果数据库不存在，则创建一个）
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MyDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
