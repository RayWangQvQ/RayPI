namespace Ray.Infrastructure.Auditing.PropertySetter
{
    /// <summary>
    /// 审计属性Setter器
    /// </summary>
    public interface IAuditPropertySetter
    {
        /// <summary>
        /// 为创建相关的审计属性赋值
        /// </summary>
        /// <param name="targetObject"></param>
        void SetCreationProperties(object targetObject);

        /// <summary>
        /// 为修改相关的审计属性赋值
        /// </summary>
        /// <param name="targetObject"></param>
        void SetModificationProperties(object targetObject);

        /// <summary>
        /// 为删除相关的审计属性赋值
        /// </summary>
        /// <param name="targetObject"></param>
        void SetDeletionProperties(object targetObject);
    }
}
