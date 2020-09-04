using System;

namespace Ray.Auditing
{
    /// <summary>
    /// 逻辑删除相关的审计字段
    /// </summary>
    public interface IHasDeletionAuditing : ILogicDeletable
    {
        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 操作人Id
        /// </summary>
        Guid? DeleterId { get; set; }
    }

    /// <summary>
    /// 逻辑删除相关的审计字段
    /// </summary>
    public interface IHasDeletionAuditing<TUser> : IHasDeletionAuditing
    {
        /// <summary>
        /// 操作人对象
        /// </summary>
        TUser Deleter { get; set; }
    }
}
