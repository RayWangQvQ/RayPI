using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RayPI.Domain.Entity;
using RayPI.Infrastructure.Treasury.Extensions;
using RayPI.Infrastructure.Treasury.Helpers;
using MediatR;
using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Ray.Domain;
using Ray.Domain.OperatorInfo;
using Ray.Infrastructure.Repository.EfCore;

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
                Content = "这是内容"
            });
        }
        #endregion
    }
}
