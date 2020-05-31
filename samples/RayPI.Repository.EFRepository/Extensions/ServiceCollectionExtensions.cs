using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ray.Infrastructure.Auditing;
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
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyRepository(this IServiceCollection services)
        {
            #region 注册数据库上下文,需要传入数据库配置(数据库类型,链接字符串等)
            //方法1:利用IConfiguration对象读取配置
            //services.AddDbContext<MyDbContext>(options => options.UseSqlServer(services.GetConfiguration()["Db:ConnStr"]));

            //方法2:利用IOptions模式读取配置(推荐)
            services.AddDbContext<MyDbContext>((serviceProvider, optionBuilder) =>
            {
                var dbOption = serviceProvider.GetRequiredService<IOptionsSnapshot<DbOption>>();
                optionBuilder.UseSqlServer(dbOption.Value.ConnStr);
            });
            #endregion

            //注册审计属性赋值器
            services.AddTransient<IAuditPropertySetter, AuditPropertySetter>();

            #region 注册仓储
            //注册实体仓储:
            services.AddAssemblyServices(Assembly.GetExecutingAssembly(), x => x.Name.EndsWith("Repository"));
            //注册泛型仓储:
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            #endregion

            return services;
        }
    }
}
