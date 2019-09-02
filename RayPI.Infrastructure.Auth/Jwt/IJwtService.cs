//本地项目包
using RayPI.Infrastructure.Auth.Models;

namespace RayPI.Infrastructure.Auth.Jwt
{
    /// <summary>
    /// Jwt服务[Interface]
    /// </summary>
    public interface IJwtService
    {
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
