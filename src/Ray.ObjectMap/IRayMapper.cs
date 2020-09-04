namespace Ray.ObjectMap
{
    /// <summary>
    /// Mapper映射器
    /// </summary>
    public interface IRayMapper : IObjectMapper
    {
        /// <summary>
        /// 自动化映射器，当没有指定的映射器时使用其实现自动映射
        /// </summary>
        IAutoObjectMapper AutoObjectMapper { get; }
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
