using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RayPI.Infrastructure.Treasury.Di;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RayPI.Repository.EFRepository.Di
{
    public static class RepositoryDiExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration config)
        {
            //注册数据库实例
            string conn = config.GetConnectionString("SqlServerDatabase");
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(conn));

            Assembly repositoryAssembly = Assembly.GetExecutingAssembly();
            services.AddAssemblyServices(repositoryAssembly, x => x.Name.EndsWith("Repository"));
            return services;
        }
    }
}
