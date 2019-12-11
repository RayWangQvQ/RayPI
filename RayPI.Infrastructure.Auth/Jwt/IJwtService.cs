//本地项目包

using System.Collections.Generic;
using RayPI.Infrastructure.Auth.Models;
using System.Security.Claims;

namespace RayPI.Infrastructure.Auth.Jwt
{
    /// <summary>
    /// Jwt服务[Interface]
    /// </summary>
    public interface IJwtService
    {
        Claim[] BuildClaims(string userName, List<string> roleName);
        string BuildToken(Claim[] claims);
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        string IssueJwt(TokenModel tokenModel);

        /// <summary>
        /// 解析jwt字符串
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        TokenModel SerializeJWT(string jwtStr);
    }
}
