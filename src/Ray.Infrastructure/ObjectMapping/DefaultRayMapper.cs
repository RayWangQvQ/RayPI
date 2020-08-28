using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ray.Infrastructure.ObjectMapping
{
    public class DefaultRayMapper : IRayMapper
    {
        public IAutoRayMapper AutoRayMapper { get; }

        protected IServiceProvider ServiceProvider { get; }

        public DefaultRayMapper(
            IServiceProvider serviceProvider,
            IAutoRayMapper autoRayMapper)
        {
            AutoRayMapper = autoRayMapper;
            ServiceProvider = serviceProvider;
        }


        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            //1.源为null，则返回null
            if (source == null)
            {
                return default;
            }

            //2.尝试从容器种获取对应mapper
            using (var scope = ServiceProvider.CreateScope())
            {
                var specificMapper = scope.ServiceProvider.GetService<IRayMapper<TSource, TDestination>>();
                if (specificMapper != null)
                {
                    return specificMapper.Map(source);
                }
            }

            //3.容器中没有，则使用自动化mapper映射器进行映射
            return AutoMap<TSource, TDestination>(source);
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
            {
                return default;
            }

            using (var scope = ServiceProvider.CreateScope())
            {
                var specificMapper = scope.ServiceProvider.GetService<IRayMapper<TSource, TDestination>>();
                if (specificMapper != null)
                {
                    return specificMapper.Map(source, destination);
                }
            }

            return AutoMap(source, destination);
        }

        protected virtual TDestination AutoMap<TSource, TDestination>(TSource source)
        {
            return AutoRayMapper.Map<TSource, TDestination>(source);
        }

        protected virtual TDestination AutoMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoRayMapper.Map<TSource, TDestination>(source, destination);
        }
    }

    public class DefaultRayMapper<TContext> : DefaultRayMapper, IRayMapper<TContext>
    {
        public DefaultRayMapper(
            IServiceProvider serviceProvider,
            IAutoRayMapper<TContext> autoRayMapper
            ) : base(
                serviceProvider,
                autoRayMapper)
        {

        }
    }
}
