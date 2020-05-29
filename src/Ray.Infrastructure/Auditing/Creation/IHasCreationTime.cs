using System;

namespace Ray.Infrastructure.Auditing.Creation
{
    /// <summary>
    /// 拥有创建时间属性
    /// </summary>
    public interface IHasCreationTime
    {
        /// <summary>
        /// Creation time.
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
