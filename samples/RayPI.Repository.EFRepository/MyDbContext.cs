using System;
using Microsoft.EntityFrameworkCore;
using RayPI.Domain.Entity;
using MediatR;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Repository.EfCore;
using System.Threading.Tasks;

namespace RayPI.Repository.EFRepository
{
    public class MyDbContext : EfDbContext<MyDbContext>
    {
        private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public MyDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public MyDbContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus)
            : base(options)
        {
        }

        #region OnConfiguring
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
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //映射关系
            base.OnModelCreating(modelBuilder);

            //设置全局默认的数据库敢纲要（不设置的话默认为dbo）
            modelBuilder.HasDefaultSchema("ray");

            //初始化一条数据
            modelBuilder.Entity<Article>().HasData(new Article("这是一条初始化的数据")
            {
                Id = Guid.NewGuid(),
                SubTitle = "来自DbContext的OnModelCreating",
                Content = "这是内容",
                CreationTime = DateTime.Now
            });
        }
        #endregion

        public override void Dispose()
        {
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }
    }
}
