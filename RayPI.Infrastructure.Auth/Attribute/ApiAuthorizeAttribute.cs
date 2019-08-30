//微软包
using Microsoft.AspNetCore.Authorization;
//本地项目包
using RayPI.Infrastructure.Auth.Enums;

namespace RayPI.Infrastructure.Auth.Attribute
{
    /// <summary>
    /// 自定义授权特性
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiAuthorizeAttribute(PolicyEnum policyEnum)
        {
            this.Policy = policyEnum.ToString();
        }
    }
}
