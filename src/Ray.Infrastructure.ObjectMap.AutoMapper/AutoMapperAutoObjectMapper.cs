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
            //todo:判断是否存在map配置，不存在则使用Helper进行映射
            return _mapper.Map<TDestination>(source);
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            //todo:判断是否存在map配置，不存在则使用Helper进行映射
            return _mapper.Map(source, destination);
        }
    }
}
