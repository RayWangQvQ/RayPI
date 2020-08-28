using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.ObjectMapping
{
    /// <summary>
    /// Mapper映射器
    /// </summary>
    public interface IRayMapper : IObjectMapper
    {
        /// <summary>
        /// Gets the underlying <see cref="IAutoRayMapper"/> object that is used for auto object mapping.
        /// </summary>
        IAutoRayMapper AutoRayMapper { get; }
    }

    /// <summary>
    /// Mapper映射器
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IRayMapper<TContext> : IRayMapper
    {

    }

    /// <summary>
    /// Mapper映射器
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public interface IRayMapper<in TSource, TDestination> : IObjectMapper<TSource, TDestination>
    {

    }
}
