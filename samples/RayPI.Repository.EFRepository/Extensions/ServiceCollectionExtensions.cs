using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Treasury.Di;
using RayPI.Repository.EFRepository.Repository;

namespace RayPI.Repository.EFRepository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyRepository(this IServiceCollection services, string connStr)
        {
            //注册数据库上下文
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connStr));

            //注册仓储
            Assembly repositoryAssembly = Assembly.GetExecutingAssembly();
            services.AddAssemblyServices(repositoryAssembly, x => x.Name.EndsWith("Repository"));

            //注册泛型仓储
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
