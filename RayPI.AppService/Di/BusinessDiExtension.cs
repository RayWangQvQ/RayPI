using Microsoft.Extensions.DependencyInjection;
using RayPI.Infrastructure.Treasury.Di;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RayPI.Business.Di
{
    public static class BusinessDiExtension
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            Assembly appServiceAssembly = Assembly.GetExecutingAssembly();
            services.AddAssemblyServices(appServiceAssembly, x => x.Name.EndsWith("Business"));
            return services;
        }
    }
}
