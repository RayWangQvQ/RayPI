using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RayPI.Repository.EFRepository.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseMyRepository(this IApplicationBuilder app)
        {
            //初始化数据库
            using var scope = app.ApplicationServices.CreateScope();
            var dc = scope.ServiceProvider.GetService<MyDbContext>();
            dc.Database.EnsureCreated();
        }
    }
}
