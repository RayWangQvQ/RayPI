using Microsoft.AspNetCore.Http;
//
using RayPI.Infrastructure.Auth.Models;
using RayPI.Infrastructure.Auth.Jwt;

namespace RayPI.Infrastructure.Auth.Operate
{
    public class OperateInfo : IOperateInfo
    {
        private readonly HttpContext _httpContext;

        private readonly IJwtService _jwtService;

        public OperateInfo(IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _jwtService = jwtService;
        }

        public string TokenStr => _httpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

        public TokenModel TokenModel => _jwtService.SerializeJWT(TokenStr);

    }
}
