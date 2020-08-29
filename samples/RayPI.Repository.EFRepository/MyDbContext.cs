using System;
using Microsoft.EntityFrameworkCore;
using RayPI.Domain.Entity;
using MediatR;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Ray.Repository.EfCore;

namespace RayPI.Repository.EFRepository
{
    public class MyDbContext : EfDbContext
    {
        private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public MyDbContext(DbContextOptions options, IServiceProvider serviceProvider)
            : base(options, serviceProvider)
        {
        }

        /// <summary>
        /// 配置数据库
        /// （该方法内配置会覆盖构造函数中传入的DbContextOptions）
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //日志：
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //映射关系
            base.OnModelCreating(modelBuilder);

            //设置全局默认的数据库敢纲要（不设置的话默认为dbo）
            modelBuilder.HasDefaultSchema("ray");
        }

        protected override void ApplyConfigurationsFromAssembly(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
        }
    }
}
