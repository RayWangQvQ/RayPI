using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using RayPI.AuthService.Jwt;
using RayPI.Treasury.Models;

namespace RayPI.Infrastructure.Auth
{
    public class OperateInfo : IOperateInfo
    {
        private HttpContext _httpContext;

        private IJwtService _jwtService;

        public OperateInfo(IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _jwtService = jwtService;
        }

        public string TokenStr => _httpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

        public TokenModel TokenModel => _jwtService.SerializeJWT(TokenStr);

    }
}
