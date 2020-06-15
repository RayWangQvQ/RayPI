using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ray.Infrastructure.Auditing.PropertySetter;
using Ray.Infrastructure.Repository.EfCore;
using Ray.Infrastructure.Security.Claims;
using Ray.Infrastructure.Security.User;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Config.Options;
using RayPI.Repository.EFRepository.Repository;

namespace RayPI.Repository.EFRepository.Extensions
{
    public static class ContainerBuilderExtension
    {
        public static void AddMyRepository(this ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            //注册数据库上下文：
            builder.Register<MyDbContext>(x =>
                {
                    var dbOption = x.Resolve<IOptionsSnapshot<DbOption>>();

                    var opt = new DbContextOptionsBuilder<MyDbContext>();
                    opt.UseSqlServer(dbOption.Value.ConnStr);

                    return new MyDbContext(opt.Options);
                })
                .AsSelf()
                .As<EfDbContext<MyDbContext>>()
                .As<DbContext>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            //注册审计属性相关服务：
            builder.RegisterType<AuditPropertySetter>()
                .AsImplementedInterfaces();
            builder.RegisterType<CurrentUser>()
                .AsImplementedInterfaces();
            builder.RegisterType<ThreadCurrentPrincipalAccessor>()
                .AsImplementedInterfaces();

            #region 注册仓储
            //注册泛型仓储:
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IBaseRepository<>));

            //利用反射扫描注册仓储:
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            #endregion
        }
    }
}
