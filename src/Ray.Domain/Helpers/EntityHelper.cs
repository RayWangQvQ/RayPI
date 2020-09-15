using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ray.Domain.Entities;
using Ray.Domain.Entities.Attributes;

namespace Ray.Domain.Helpers
{
    public static class EntityHelper
    {
        private static readonly ConcurrentDictionary<string, PropertyInfo> CachedIdProperties =
            new ConcurrentDictionary<string, PropertyInfo>();

        /// <summary>
        /// 尝试为实体Id赋值
        /// </summary>
        /// <typeparam name="TKey">Id类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="idFactory">Id生成工厂</param>
        /// <param name="checkForDisableIdGenerationAttribute">是否检查Id属性的忽略自动生成特性DisableIdGenerationAttribute</param>
        public static void TrySetId<TKey>(
           IEntity<TKey> entity,
           Func<TKey> idFactory,
           bool checkForDisableIdGenerationAttribute = false)
        {
            PropertyInfo property = CachedIdProperties.GetOrAdd(
                $"{entity.GetType().FullName}-{checkForDisableIdGenerationAttribute}",
                () =>
                {
                    PropertyInfo idProperty = entity
                        .GetType()
                        .GetProperties()
                        .FirstOrDefault(x => x.Name == nameof(entity.Id) &&
                                             x.GetSetMethod(true) != null);

                    if (idProperty == null)
                    {
                        return null;
                    }

                    if (checkForDisableIdGenerationAttribute
                    && idProperty.IsDefined(typeof(DisableIdGenerationAttribute), true))
                    {
                        return null;
                    }

                    return idProperty;
                });

            property?.SetValue(entity, idFactory());
        }
    }
}
