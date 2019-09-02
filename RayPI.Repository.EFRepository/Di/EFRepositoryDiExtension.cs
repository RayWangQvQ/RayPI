//系统包
using System.Reflection;
//微软包
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//本地项目包
using RayPI.Infrastructure.Treasury.Di;

namespace RayPI.Repository.EFRepository.Di
{
    public static class EFRepositoryDiExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connStr)
        {
            //注册数据库实例
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connStr));

            Assembly repositoryAssembly = Assembly.GetExecutingAssembly();
            services.AddAssemblyServices(repositoryAssembly, x => x.Name.EndsWith("Repository"));
            return services;
        }
    }
}
