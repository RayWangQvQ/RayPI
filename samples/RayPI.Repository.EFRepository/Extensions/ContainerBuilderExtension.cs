using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Ray.Infrastructure.Repository.EfCore;

namespace RayPI.Repository.EFRepository.Extensions
{
    public static class ContainerBuilderExtension
    {
        public static void AddMyRepository(this ContainerBuilder builder)
        {
            /*
            builder.RegisterType<MyDbContext>()
                .AsSelf()
                .As<EfDbContext<MyDbContext>>()
                .As<DbContext>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
                */
        }
    }
}
