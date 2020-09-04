using AutoMapper;

namespace Ray.ObjectMap.AutoMapper
{
    public class AutoMapperHelper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return MapCore<TSource, TDestination>(source);
        }

        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            destination = MapCore<TSource, TDestination>(source);
            return destination;
        }

        private static TDestination MapCore<TSource, TDestination>(TSource source)
        {
            //todo:考虑集合和泛型
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            TDestination des = mapper.Map<TSource, TDestination>(source);
            return des;
        }
    }
}
