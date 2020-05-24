using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Extensions.Linq
{
    public static class EnumerableExtension
    {
        /// <summary>each循环</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iEnumerable"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> iEnumerable, Action<T> action)
        {
            if (iEnumerable == null)
                return;
            foreach (T i in iEnumerable)
                action(i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iEnumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void Each<T>(this IEnumerable iEnumerable, Action<T> action)
        {
            if (iEnumerable == null)
                return;
            foreach (object i in iEnumerable)
                action((T)i);
        }
    }
}
