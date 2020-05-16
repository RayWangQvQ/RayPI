//系统包
using System;

namespace RayPI.Infrastructure.Treasury.Interfaces
{
    public interface IEntityBaseAutoSetter
    {
        /// <summary>创建人姓名</summary>
        /// <value>The name of the create.</value>
        string CreateName { get; }

        /// <summary>创建人Id</summary>
        /// <value>The create identifier.</value>
        long CreateId { get; }

        /// <summary>创建时间</summary>
        /// <value>The create time.</value>
        DateTime CreateTime { get; }

        /// <summary>更新人姓名</summary>
        /// <value>The name of the update.</value>
        string UpdateName { get; }

        /// <summary>更新人Id</summary>
        /// <value>The update identifier.</value>
        long UpdateId { get; }

        /// <summary>更新时间</summary>
        /// <value>The update time.</value>
        DateTime UpdateTime { get; }
    }
}
