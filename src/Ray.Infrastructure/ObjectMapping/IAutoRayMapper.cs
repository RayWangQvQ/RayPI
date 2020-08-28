using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.ObjectMapping
{
    /// <summary>
    /// 自动化Mapper映射器
    /// </summary>
    public interface IAutoRayMapper : IObjectMapper
    {

    }

    /// <summary>
    /// 自动化Mapper映射器
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IAutoRayMapper<TContext> : IAutoRayMapper
    {

    }
}
