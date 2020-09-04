using Microsoft.Extensions.DependencyInjection;

namespace Ray.ObjectMap
{
    public static class RayMapperModule
    {
        public static IServiceCollection AddRayMapper(this IServiceCollection services)
        {
            services.AddSingleton<IRayMapper, DefaultRayMapper>();
            services.AddSingleton<IAutoObjectMapper, NotImplementedAutoObjectMapper>();

            return services;
        }
    }
}
