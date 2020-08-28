using AutoMapper;

namespace Ray.Infrastructure.ObjectMap.AutoMapper
{
    public class AutoMapperAutoRayMapper : IAutoObjectMapper
    {
        private readonly IMapper _mapper;

        public AutoMapperAutoRayMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}
