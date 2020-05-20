using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Domain
{
    /// <summary>
    /// 实体操作人信息
    /// （添加信息、更新信息）
    /// </summary>
    public interface IEntityOperatorInfo
    {
        #region 创建信息
        /// <summary>创建人姓名</summary>
        /// <value>The name of the create.</value>
        string CreateName { get; set; }

        /// <summary>创建人Id</summary>
        /// <value>The create identifier.</value>
        long? CreateId { get; set; }

        /// <summary>创建时间</summary>
        /// <value>The create time.</value>
        DateTime? CreateTime { get; set; }
        #endregion

        #region 更新信息
        /// <summary>更新人姓名</summary>
        /// <value>The name of the update.</value>
        string UpdateName { get; set; }

        /// <summary>更新人Id</summary>
        /// <value>The update identifier.</value>
        long? UpdateId { get; set; }

        /// <summary>更新时间</summary>
        /// <value>The update time.</value>
        DateTime? UpdateTime { get; set; }
        #endregion
    }
}
