using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Auditing
{
    public interface IMayHaveCreator<TCreator>
    {
        /// <summary>
        /// 创建人
        /// </summary>
        TCreator Creator { get; set; }
    }

    /// <summary>
    /// Standard interface for an entity that MAY have a creator.
    /// </summary>
    public interface IMayHaveCreator
    {
        /// <summary>
        /// 创建人Id
        /// </summary>
        Guid? CreatorId { get; set; }
    }
}
