using System;

namespace Ray.Auditing
{
    /// <summary>
    /// 创建相关的审计字段
    /// </summary>
    public interface IHasCreationAuditing
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        Guid? CreatorId { get; set; }
    }

    /// <summary>
    /// 创建相关的审计字段
    /// </summary>
    public interface IHasCreationAuditing<TCreator> : IHasCreationAuditing
    {
        /// <summary>
        /// 创建人对象
        /// </summary>
        TCreator Creator { get; set; }
    }
}
