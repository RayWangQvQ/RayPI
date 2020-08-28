using AutoMapper;

namespace Ray.Infrastructure.ObjectMap.AutoMapper
{
    /// <summary>
    /// 使用AutoMapper实现IAutoObjectMapper
    /// </summary>
    public class AutoMapperAutoObjectMapper : IAutoObjectMapper
    {
        private readonly IMapper _mapper;

        public AutoMapperAutoObjectMapper(IMapper mapper)
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
