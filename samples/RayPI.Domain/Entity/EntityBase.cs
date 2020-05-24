using System;
using Ray.Domain;
using RayPI.Infrastructure.Treasury.Interfaces;

namespace RayPI.Domain.Entity
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class EntityBase : IntegratedEntity, IAggregateRoot
    {
        #region 创建信息
        /// <summary>创建人姓名</summary>
        /// <value>The name of the create.</value>
        public string CreateName { get; set; } = string.Empty;

        /// <summary>创建人Id</summary>
        /// <value>The create identifier.</value>
        public long? CreateId { get; set; }

        /// <summary>创建时间</summary>
        /// <value>The create time.</value>
        public DateTime? CreateTime { get; set; }
        #endregion

        #region 更新信息
        /// <summary>更新人姓名</summary>
        /// <value>The name of the update.</value>
        public string UpdateName { get; set; } = string.Empty;

        /// <summary>更新人Id</summary>
        /// <value>The update identifier.</value>
        public long? UpdateId { get; set; }

        /// <summary>更新时间</summary>
        /// <value>The update time.</value>
        public DateTime? UpdateTime { get; set; }
        #endregion

        #region 逻辑删除信息
        /// <summary>数据状态</summary>
        /// <value>The state of the data.</value>
        public bool IsDeleted { get; set; }

        /// <summary>删除时间/// </summary>
        /// <value>The delete time.</value>
        public DateTime? DeleteTime { get; set; }
        #endregion

        /// <summary>基础字段设置方法</summary>
        /// <value>The automatic setter.</value>
        public IEntityBaseAutoSetter AutoSetter { get; set; }
    }
}
