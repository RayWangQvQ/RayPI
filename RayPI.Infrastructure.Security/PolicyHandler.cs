//系统包

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
//微软包
//三方包
//本地项目包

namespace RayPI.Infrastructure.Security
{
    /// <summary>
    /// 自定义授权处理
    /// </summary>
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        /// <summary>
        /// 授权方式（cookie, bearer, oauth, openid）
        /// </summary>
        private readonly IAuthenticationSchemeProvider _schemes;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="schemes"></param>
        public PolicyHandler(IAuthenticationSchemeProvider schemes)
        {
            _schemes = schemes;
        }

        /// <summary>
        /// 授权处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            object resource = context.Resource;
            HttpContext httpContext = (context.Resource as AuthorizationFilterContext)?.HttpContext;
            if (httpContext == null) return;

            //获取授权方式
            AuthenticationScheme defaultAuthenticate = await _schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate == null)
            {
                context.Fail();
                return;
            }

            //验证token（包括过期时间）
            AuthenticateResult result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
            if (!result.Succeeded)
            {
                context.Fail();
                return;
            }

            httpContext.User = result.Principal;

            context.Succeed(requirement);
        }
    }
}
