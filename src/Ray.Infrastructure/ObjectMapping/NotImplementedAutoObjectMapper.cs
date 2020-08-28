using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.ObjectMapping
{
    public sealed class NotImplementedAutoObjectMapper : IAutoRayMapper
    {
        public IAutoRayMapper AutoObjectMapper => throw new NotImplementedException();

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException($"Can not map from given object ({source}) to {typeof(TDestination).AssemblyQualifiedName}.");
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException($"Can no map from {typeof(TSource).AssemblyQualifiedName} to {typeof(TDestination).AssemblyQualifiedName}.");
        }
    }
}
