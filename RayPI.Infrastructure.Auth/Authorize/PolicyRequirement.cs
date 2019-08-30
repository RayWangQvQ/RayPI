//微软包
using Microsoft.AspNetCore.Authorization;

namespace RayPI.Infrastructure.Auth.Authorize
{
    /// <summary>
    /// 授权策略
    /// </summary>
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public PolicyRequirement(string role)
        {
            this.RequireRoles = role.Split(',');
        }

        public string[] RequireRoles { get; set; }
    }
}
