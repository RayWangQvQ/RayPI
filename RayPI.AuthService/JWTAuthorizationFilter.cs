using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
//
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
//
using RayPI.Treasury.Models;


namespace RayPI.AuthService
{
    /// <summary>
    /// 授权中间件
    /// </summary>
    public class JwtAuthorizationFilter
    {
        private readonly RequestDelegate _next;
        //private readonly IServiceCollection _services;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public JwtAuthorizationFilter(RequestDelegate next)
        {
            _next = next;
            //_services = services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            //检测是否包含'Authorization'请求头，如果不包含则直接放行
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }

            string tokenHeader = httpContext.Request.Headers["Authorization"];
            tokenHeader = tokenHeader.Substring("Bearer ".Length).Trim();

            TokenModel tm = JwtHelper.SerializeJWT(tokenHeader);

            //注册tokenModel
            //_services.AddScoped(x => tm);

            //授权
            var claimList = new List<Claim>();
            var claim = new Claim(ClaimTypes.Role, tm.Role);
            claimList.Add(claim);
            var identity = new ClaimsIdentity(claimList);
            var principal = new ClaimsPrincipal(identity);
            httpContext.User = principal;

            return _next(httpContext);
        }
    }
}
