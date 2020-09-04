using AutoMapper;

namespace Ray.Infrastructure.ObjectMap.AutoMapper
{
    public class AutoMapperHelper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            TDestination info = mapper.Map<TSource, TDestination>(source);
            return info;
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            destination = Map<TSource, TDestination>(source);
            return destination;
        }
    }
}
