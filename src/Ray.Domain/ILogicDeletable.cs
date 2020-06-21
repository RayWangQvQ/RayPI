using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Domain
{
    /// <summary>
    /// 实体可逻辑删除
    /// </summary>
    public interface ILogicDeletable
    {
        /// <summary>是否被逻辑删除</summary>
        /// <value>The state of the data.</value>
        bool IsDeleted { get; set; }

        /// <summary>删除时间</summary>
        /// <value>The delete time.</value>
        DateTime? DeleteTime { get; set; }
    }
}
