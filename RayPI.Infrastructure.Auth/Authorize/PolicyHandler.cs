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
using System.Security.Claims;

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

        private readonly IRolePermissionService _rolePermissionService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="schemes"></param>
        public PolicyHandler(IAuthenticationSchemeProvider schemes, IRolePermissionService rolePermissionService)
        {
            _schemes = schemes;
            _rolePermissionService = rolePermissionService;
        }

        /// <summary>
        /// 授权处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RayRequirement requirement)
        {
            var claims = context.User.Claims;
            string roleCode = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role.ToString())?.Value;

            var permissions = _rolePermissionService.GetPermissions(roleCode);

            if (!permissions.Any(x => x.OperateCode == requirement.Operate && x.ResourceCode == requirement.Resource))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
