//系统包
using System.Linq;
using System.Threading.Tasks;
//微软包
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
//三方包
using Newtonsoft.Json;
//本地项目包
using RayPI.Infrastructure.Auth.Enums;
using RayPI.Infrastructure.Auth.Models;

namespace RayPI.Infrastructure.Auth.Authorize
{
    /// <summary>
    /// 自定义授权处理
    /// </summary>
    public class PolicyHandler : AuthorizationHandler<RayRequirement>
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
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RayRequirement requirement)
        {
            var principal = context.User;

            /*
            //判断角色
            string url = httpContext.Request.Path.Value.ToLower();
            string tokenModelJsonStr = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypeEnum.TokenModel.ToString())?.Value;
            TokenModel tm = JsonConvert.DeserializeObject<TokenModel>(tokenModelJsonStr);

            if (!requirement.RequireRoles.Contains(tm.Role))
            {
                context.Fail();
                return;
            }
            */

            context.Succeed(requirement);
        }
    }
}
