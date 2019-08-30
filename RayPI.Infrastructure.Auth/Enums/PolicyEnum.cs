
namespace RayPI.Infrastructure.Auth.Enums
{
    /// <summary>
    /// 授权策略模式枚举
    /// </summary>
    public enum PolicyEnum
    {
        RequireRoleOfClient,
        RequireRoleOfAdmin,
        RequireRoleOfAdminOrClient
    }
}
