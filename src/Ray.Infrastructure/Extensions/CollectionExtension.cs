using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public static class CollectionExtension
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }
    }
}
