//微软包
using Microsoft.AspNetCore.Http;
//本地项目包
using RayPI.Infrastructure.Auth.Models;
using RayPI.Infrastructure.Auth.Jwt;

namespace RayPI.Infrastructure.Auth.Operate
{
    /// <summary>
    /// 操作人信息
    /// </summary>
    public class OperateInfo : IOperateInfo
    {
        private readonly HttpContext _httpContext;

        private readonly IJwtService _jwtService;

        public OperateInfo(IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _jwtService = jwtService;
        }

        /// <summary>
        /// 令牌字符串
        /// </summary>
        public string TokenStr => _httpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
        /// <summary>
        /// 令牌
        /// </summary>
        public TokenModel TokenModel => _jwtService.SerializeJWT(TokenStr);

    }
}
