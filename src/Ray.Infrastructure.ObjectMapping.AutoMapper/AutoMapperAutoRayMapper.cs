using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Ray.Infrastructure.ObjectMapping.AutoMapper
{
    public class AutoMapperAutoRayMapper : IAutoRayMapper
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

    public class AutoMapperAutoRayMapper<TContext> : AutoMapperAutoRayMapper, IAutoRayMapper<TContext>
    {
        public AutoMapperAutoRayMapper(IMapper mapperAccessor)
            : base(mapperAccessor)
        {
        }
    }
}
