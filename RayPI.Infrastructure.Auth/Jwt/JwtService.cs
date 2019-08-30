//系统包
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//微软包
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//三方包
using Newtonsoft.Json;
//本地项目包
using RayPI.Infrastructure.Auth.Enums;
using RayPI.Infrastructure.Auth.Models;
using RayPI.Infrastructure.Config.Model;

namespace RayPI.Infrastructure.Auth.Jwt
{
    /// <summary>
    /// Jwt服务
    /// </summary>
    public class JwtService : IJwtService
    {
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtService(JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

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
                //new Claim(JwtRegisteredClaimNames.Iss,"RayPI"),//颁发人
                //new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),//用户Id
                //new Claim(ClaimTypes.Role, tokenModel.Role),//身份
                //new Claim("proj", tokenModel.Project),//项目
                //new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToString(),ClaimValueTypes.Integer64),
                new Claim(ClaimTypeEnum.TokenModel.ToString(),JsonConvert.SerializeObject(tokenModel))
            };

            //过期时间
            double expMin = 0;
            switch (tokenModel.TokenType)
            {
                case TokenTypeEnum.Web:
                    expMin = jwtConfig.WebExp;
                    break;
                case TokenTypeEnum.App:
                    expMin = jwtConfig.AppExp;
                    break;
                case TokenTypeEnum.MiniProgram:
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

            var jwt = new JwtSecurityToken(issuer: "RayPI",
                claims: claims,
                expires: expTime,//过期时间
                signingCredentials: creds);

            var encodedJwt = _jwtSecurityTokenHandler.WriteToken(jwt);

            return $"{JwtBearerDefaults.AuthenticationScheme} {encodedJwt}";
        }

        /// <summary>
        /// 解析jwt字符串
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public TokenModel SerializeJWT(string jwtStr)
        {
            var tm = new TokenModel();
            JwtSecurityToken jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(jwtStr);

            try
            {
                jwtToken.Payload.TryGetValue("TokenModel", out object tokenModelObj);
                tm = JsonConvert.DeserializeObject<TokenModel>(tokenModelObj?.ToString());
            }
            catch (Exception)
            {
                // ignored
            }

            return tm;
        }
    }
}
