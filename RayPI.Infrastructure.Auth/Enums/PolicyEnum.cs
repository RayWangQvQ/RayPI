
namespace RayPI.Infrastructure.Auth.Enums
{
    /// <summary>
    /// 授权策略模式枚举
    /// </summary>
    public enum PolicyEnum
    {
        /// <summary>
        /// 开放接口，不需要授权
        /// </summary>
        Free,
        /// <summary>
        /// 仅对客户端用户开放
        /// </summary>
        RequireRoleOfClient,
        /// <summary>
        /// 仅对后台用户开放
        /// </summary>
        RequireRoleOfAdmin,
        /// <summary>
        /// 仅对客户端或后台用户开放
        /// </summary>
        RequireRoleOfAdminOrClient
    }
}
