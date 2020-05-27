using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ray.Domain.Entities;

namespace Ray.Domain.Helpers
{
    public static class EntityHelper
    {
        private static readonly ConcurrentDictionary<string, PropertyInfo> CachedIdProperties =
            new ConcurrentDictionary<string, PropertyInfo>();

        public static void TrySetId<TKey>(
           IEntity<TKey> entity,
           Func<TKey> idFactory,
           bool checkForDisableIdGenerationAttribute = false)
        {
            var property = CachedIdProperties.GetOrAdd(
                $"{entity.GetType().FullName}-{checkForDisableIdGenerationAttribute}", () =>
                {
                    var idProperty = entity
                        .GetType()
                        .GetProperties()
                        .FirstOrDefault(x => x.Name == nameof(entity.Id) &&
                                             x.GetSetMethod(true) != null);

                    if (idProperty == null)
                    {
                        return null;
                    }

                    if (checkForDisableIdGenerationAttribute)
                    {
                        return null;
                    }

                    return idProperty;
                });

            property?.SetValue(entity, idFactory());
        }
    }
}
