//系统包
using System.Reflection;
//微软包
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RayPI.Domain.IRepository;
//本地项目包
using RayPI.Infrastructure.Treasury.Di;
using RayPI.Repository.EFRepository.Repository;

namespace RayPI.Repository.EFRepository.Di
{
    public static class EFRepositoryDiExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string connStr)
        {
            //注册数据库实例
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
