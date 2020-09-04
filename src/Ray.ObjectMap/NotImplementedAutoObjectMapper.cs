using System;

namespace Ray.ObjectMap
{
    public sealed class NotImplementedAutoObjectMapper : IAutoObjectMapper
    {
        public IAutoObjectMapper AutoObjectMapper => throw new NotImplementedException();

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
