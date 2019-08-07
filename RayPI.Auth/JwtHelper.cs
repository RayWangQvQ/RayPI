using Microsoft.IdentityModel.Tokens;
using RayPI.ConfigService.ConfigModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RayPI.Treasury.Models;


namespace RayPI.AuthService
{
    public static class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJWT(TokenModel tokenModel, JwtAuthConfigModel jwtConfig)
        {
            var dateTime = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),//用户Id
                new Claim("Role", tokenModel.Role),//身份
                new Claim("Project", tokenModel.Project),//身份
                new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToString(),ClaimValueTypes.Integer64)
            };
            //秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //过期时间
            double exp = 0;
            switch (tokenModel.TokenType)
            {
                case "Web":
                    exp = jwtConfig.WebExp;
                    break;
                case "App":
                    exp = jwtConfig.AppExp;
                    break;
                case "MiniProgram":
                    exp = jwtConfig.MiniProgramExp;
                    break;
                case "Other":
                    exp = jwtConfig.OtherExp;
                    break;
            }
            var jwt = new JwtSecurityToken(
                issuer: "RayPI",
                claims: claims, //声明集合
                expires: dateTime.AddHours(exp),
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModel SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            try
            {
                jwtToken.Payload.TryGetValue("Role", out object role);
                jwtToken.Payload.TryGetValue("Project", out object project);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModel
            {
                Uid = long.Parse(jwtToken.Id),
                Role = new object().ToString(),
                Project = new object().ToString(),
                TokenString = jwtStr
            };
            return tm;
        }
    }
}
