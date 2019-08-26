using RayPI.ConfigService.ConfigModel;
using RayPI.Treasury.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.AuthService.Jwt
{
    public interface IJwtService
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        string IssueJWT(TokenModel tokenModel, JwtAuthConfigModel jwtConfig);

        /// <summary>
        /// 解析jwt字符串
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        TokenModel SerializeJWT(string jwtStr);
    }
}
