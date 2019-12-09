//微软包
using Microsoft.AspNetCore.Authorization;

namespace RayPI.Infrastructure.Auth.Authorize
{
    /// <summary>
    /// 授权策略
    /// </summary>
    public class RayRequirement : IAuthorizationRequirement
    {
        public RayRequirement(bool isNeedAuthorizeds = true)
        {
            this.IsNeedAuthorized = isNeedAuthorizeds;
        }
        public RayRequirement(string role, bool isNeedAuthorizeds = true)
        {
            this.RequireRoles = role.Split(',');
            this.IsNeedAuthorized = isNeedAuthorizeds;
        }

        public RayRequirement(string operate, string resource)
        {
            Operate = operate;
            Resource = resource;
        }

        public bool IsNeedAuthorized { get; set; }

        public string[] RequireRoles { get; set; }

        public string Operate { get; set; }
        public string Resource { get; set; }
    }
}
