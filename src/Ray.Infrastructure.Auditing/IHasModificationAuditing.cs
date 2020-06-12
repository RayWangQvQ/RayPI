using System;

namespace Ray.Infrastructure.Auditing
{
    /// <summary>
    /// 编辑相关的审计字段
    /// </summary>
    public interface IHasModificationAuditing
    {
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后编辑人Id
        /// </summary>
        Guid? LastModifierId { get; set; }
    }

    /// <summary>
    /// 编辑相关的审计字段
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IHasModificationAuditing<TUser> : IHasModificationAuditing
    {
        /// <summary>
        /// 最后编辑人对象
        /// </summary>
        TUser LastModifier { get; set; }
    }
}
