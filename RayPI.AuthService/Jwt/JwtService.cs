using Microsoft.IdentityModel.Tokens;
using RayPI.ConfigService.ConfigModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RayPI.Treasury.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RayPI.AuthService.Jwt;

namespace RayPI.AuthService
{
    public class JwtService : IJwtServicecs
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public string IssueJWT(TokenModel tokenModel, JwtAuthConfigModel jwtConfig)
        {
            var dateTime = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss,"RayPI"),//颁发人
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),//用户Id
                new Claim(ClaimTypes.Role, tokenModel.Role),//身份
                new Claim("proj", tokenModel.Project),//项目
                new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToString(),ClaimValueTypes.Integer64)
            };

            //过期时间
            double expMin = 0;
            switch (tokenModel.TokenType)
            {
                case "Web":
                    expMin = jwtConfig.WebExp;
                    break;
                case "App":
                    expMin = jwtConfig.AppExp;
                    break;
                case "MiniProgram":
                    expMin = jwtConfig.MiniProgramExp;
                    break;
                default:
                    expMin = jwtConfig.OtherExp;
                    break;
            }

            DateTime expTime = dateTime.AddMinutes(expMin);

            //秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(claims: claims,
                expires: expTime,//过期时间
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            //用户标识
            //var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //identity.AddClaims(claims);

            return $"{JwtBearerDefaults.AuthenticationScheme} {encodedJwt}";
        }

        /// <summary>
        /// 解析jwt字符串
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public TokenModel SerializeJWT(string jwtStr)
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
                //TokenString = jwtStr
            };
            return tm;
        }
    }
}
