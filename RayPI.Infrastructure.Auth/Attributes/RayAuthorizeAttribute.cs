//微软包
using Microsoft.AspNetCore.Authorization;
using RayPI.Infrastructure.Auth.Enums;
//本地项目包

namespace RayPI.Infrastructure.Auth.Attributes
{
    /// <summary>
    /// 自定义授权特性
    /// </summary>
    public class RayAuthorizeAttribute : AuthorizeAttribute
    {
        public RayAuthorizeAttribute(AuthPolicyEnum authPolicyEnum)
        {
            this.Policy = authPolicyEnum.ToString();
        }
    }
}
