using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
//
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RayPI.Infrastructure.Auth.Jwt;
using RayPI.Infrastructure.Auth.Models;
//
using RayPI.Treasury.Models;


namespace RayPI.Infrastructure.Auth.Authorize
{
    /// <summary>
    /// 授权中间件
    /// </summary>
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private TokenModel _tm;
        private IJwtService _jwtServicecs;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public JwtAuthorizationMiddleware(RequestDelegate next,
            IJwtService jwtServicecs,
            IServiceProvider serviceProvider)
        {
            _next = next;
            _jwtServicecs = jwtServicecs;
            try
            {
                _tm = serviceProvider.GetService<TokenModel>();
            }
            catch (Exception)
            {
            }
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

            _tm = _jwtServicecs.SerializeJWT(tokenHeader);

            //授权
            var claimList = new List<Claim>();
            var claim = new Claim(ClaimTypes.Role, _tm.Role);
            claimList.Add(claim);
            var identity = new ClaimsIdentity(claimList);
            var principal = new ClaimsPrincipal(identity);
            httpContext.User = principal;

            return _next(httpContext);
        }
    }
}
