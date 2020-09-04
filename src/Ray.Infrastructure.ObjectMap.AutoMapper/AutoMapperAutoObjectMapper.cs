using AutoMapper;

namespace Ray.ObjectMap.AutoMapper
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
            //todo:是否需要记录日志
            if (_mapper.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() != null)
                return _mapper.Map<TDestination>(source);

            return AutoMapperHelper.Map<TSource, TDestination>(source);
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            //todo:是否需要记录日志
            if (_mapper.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() != null)
                return _mapper.Map(source, destination);

            return AutoMapperHelper.Map<TSource, TDestination>(source, destination);
        }
    }
}
