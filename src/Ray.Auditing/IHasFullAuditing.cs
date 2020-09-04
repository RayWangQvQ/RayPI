namespace Ray.Auditing
{
    /// <summary>
    /// 创建 && 编辑 && 逻辑删除相关的审计字段
    /// </summary>
    public interface IHasFullAuditing : IHasCreationAuditing, IHasModificationAuditing, IHasDeletionAuditing
    {
    }

    /// <summary>
    /// 创建 && 编辑 && 逻辑删除相关的审计字段
    /// </summary>
    public interface IHasFullAuditing<TUser> : IHasCreationAuditing<TUser>, IHasModificationAuditing<TUser>, IHasDeletionAuditing<TUser>
    {
    }
}
