using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Auditing
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
