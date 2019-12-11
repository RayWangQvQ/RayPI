//微软包
using Microsoft.AspNetCore.Authorization;

namespace RayPI.Infrastructure.Auth.Authorize
{
    /// <summary>
    /// 接口验证所需要的必要条件
    /// </summary>
    public class RayRequirement : IAuthorizationRequirement
    {
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
