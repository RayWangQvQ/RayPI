//微软包
using Microsoft.AspNetCore.Authorization;

namespace RayPI.Infrastructure.Auth.Authorize
{
    /// <summary>
    /// 授权策略
    /// </summary>
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public PolicyRequirement(bool isNeedAuthorizeds = true)
        {
            this.IsNeedAuthorized = isNeedAuthorizeds;
        }
        public PolicyRequirement(string role, bool isNeedAuthorizeds = true)
        {
            this.RequireRoles = role.Split(',');
            this.IsNeedAuthorized = isNeedAuthorizeds;
        }

        public bool IsNeedAuthorized { get; set; }

        public string[] RequireRoles { get; set; }
    }
}
