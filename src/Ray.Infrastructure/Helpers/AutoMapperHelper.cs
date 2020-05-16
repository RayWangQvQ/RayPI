using AutoMapper;

namespace Ray.Infrastructure.Helpers
{
    public class AutoMapperHelper
    {
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TTarget>());
            var mapper = config.CreateMapper();
            TTarget info = mapper.Map<TSource, TTarget>(source);
            return info;
        }
    }
}
