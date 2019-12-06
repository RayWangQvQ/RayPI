using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RayPI.Infrastructure.Security.Interface;
using RayPI.Infrastructure.Security.Models;
using RayPI.Infrastructure.Security.Services;

namespace RayPI.Infrastructure.Security
{
    public class RoleMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly EncryptionHash hash = new EncryptionHash();
        private IRoleEventsHandler _roleEventsHandler;
        private ManaRole _manaRole = new ManaRole();

        int StatusCode = 401;
        string loginfailed;

        public RoleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRoleEventsHandler roleEventsHandler)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Endpoint endpoint = context.GetEndpoint();
            // IMPORTANT: Changes to authorization logic should be mirrored in MVC's AuthorizeFilter
            IReadOnlyList<IAuthorizeData> authorizeData = endpoint?.Metadata.GetOrderedMetadata<IAuthorizeData>() ?? Array.Empty<IAuthorizeData>();

            // 没 [Authorize]， 不拦截
            if (authorizeData.Count == 0)
            {
                await _next(context);
                return;
            }

            // 有 [AllowAnonymous]，也不拦截
            if (endpoint.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }

            _roleEventsHandler = roleEventsHandler;
            // 进来时
            var result = await AuthorizationService(context);
            if (result == false)
            {
                context.Response.StatusCode = StatusCode;
                context.Response.Headers.Add("WWW-Authenticate", new Microsoft.Extensions.Primitives.StringValues(loginfailed));
                return;
            }
            await _next(context);
        }

        private async Task<bool> AuthorizationService(HttpContext context)
        {
            string tokenStr = context.Request.Headers["Authorization"].ToString();
            string requestUrl = context.Request.Path.Value;

            bool isCan = false;
            bool isDecryption = false;
            JwtSecurityToken jst = new JwtSecurityToken();

            // Token是否有效
            isCan = hash.IsCanReadToken(tokenStr);
            try
            {
                // 从 Token 里面解码出 JwtSecurityToken
                jst = hash.GetJwtSecurityToken(tokenStr);
                isDecryption = true;
            }
            catch { }

            var claims = hash.GetClaims(jst);
            var aud = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud).Value;
            string userName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            string roleName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            EventsInfo info = new EventsInfo
            {
                UserName = userName,
                RoleName = roleName,
                Url = requestUrl,
                Issuer = jst.Issuer,
                Audience = jst.Audiences,
                Token = tokenStr
            };

            /// 由于已经使用 app.UseAuthorization();，在此之前已经校验完毕，不需要这里在此校验/
            //// 未携带token时
            //if (string.IsNullOrWhiteSpace(tokenStr))
            //{
            //    if (AuthConfig.model.IsLoginAction)
            //    {
            //        context.Response.Redirect(AuthConfig.model.LoginAction);
            //    }
            //    Thread NoToken = new Thread(new ParameterizedThreadStart(_roleEventsHandler.NoToken));
            //    NoToken.Start(info);
            //    return false;
            //}


            // 如果是无效的Token
            if (!isCan)
            {
                loginfailed = AuthConfig.model.scheme.TokenEbnormal;
                Thread TokenEbnormal = new Thread(new ParameterizedThreadStart(_roleEventsHandler.TokenAbnormal));
                TokenEbnormal.Start(info);
                return false;
            }
            if (!isDecryption)
            {
                Thread TokenEbnormal = new Thread(new ParameterizedThreadStart(_roleEventsHandler.TokenAbnormal));
                TokenEbnormal.Start(info);
                return false;
            }


            // 校验 颁发主体
            if (!(jst.Issuer == AuthConfig.model.Issuer || aud != AuthConfig.model.Audience))
            {
                loginfailed = AuthConfig.model.scheme.TokenIssued;
                Thread TokenIssued = new Thread(new ParameterizedThreadStart(_roleEventsHandler.TokenIssued));
                TokenIssued.Start(info);
                return false;
            }

            // 由于已经使用 app.UseAuthorization();，在此之前已经校验时间完毕，不需要这里在此校验
            //// 校验过期时间
            //long nowTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            //// 有效期
            //var expiration = Convert.ToInt64(claims.FirstOrDefault(x => x.Type == ClaimTypes.Expiration).Value);
            //// 颁发时间
            //long issued = expiration + Convert.ToInt64(claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Iat).Value);

            //// 令牌已过期
            //if (issued < nowTime)
            //{
            //    await _roleEventsHandler.TokenTime(requestUrl, issued, expiration);
            //    return false;
            //}



            // 角色无效
            // 可能后台已经删除了此角色，或者用户被取消此角色
            if (!_manaRole.IsUserToRole(userName, roleName))
            {
                loginfailed = AuthConfig.model.scheme.NoPermissions;
                Thread NoPermissions = new Thread(new ParameterizedThreadStart(_roleEventsHandler.NoPermissions));
                NoPermissions.Start(info);
                return false;
            }

            // 是否有访问此 API 的权限
            RoleModel apiResource = _manaRole.GetRoleBeApis(roleName);
            if (apiResource == null)
            {
                loginfailed = AuthConfig.model.scheme.NoPermissions;
                Thread NoPermissions = new Thread(new ParameterizedThreadStart(_roleEventsHandler.NoPermissions));
                NoPermissions.Start(info);
                return false;
            }

            bool isHas = apiResource.Apis.Any(x => x.ApiUrl.ToLower() == requestUrl.ToLower());

            if (!isHas)
            {
                if (AuthConfig.model.IsDeniedAction == true)
                {
                    //无权限时跳转到某个页面
                    context.Response.Redirect(AuthConfig.model.DeniedAction);
                }
                return false;
            }
            new Thread(new ParameterizedThreadStart(_roleEventsHandler.Success)).Start(info);
            await _roleEventsHandler.End(context);
            return true;
        }
    }
}
