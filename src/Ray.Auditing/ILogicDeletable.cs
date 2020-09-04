namespace Ray.Auditing
{
    /// <summary>
    /// 用于标记实体允许逻辑删除
    /// （被标记的实体不会真的被删除，只会将其IsDeleted属性设置为true后存入数据库，与此同时，查询时框架也会自动过滤掉被逻辑删除的数据）
    /// </summary>
    public interface ILogicDeletable
    {
        /// <summary>
        /// 是否被逻辑删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
