using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ray.Infrastructure.DI;

namespace RayPI.Repository.EFRepository.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseMyRepository(this IApplicationBuilder app)
        {
            //初始化数据库（如果数据库不存在，则创建一个）
            //using var scope = app.ApplicationServices.CreateScope();
            using var scope = RayContainer.ServiceProviderRoot.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<MyDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}
