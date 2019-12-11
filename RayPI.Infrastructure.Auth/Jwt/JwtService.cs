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

namespace RayPI.Infrastructure.Auth.Jwt
{
    /// <summary>
    /// Jwt服务
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly JwtOption _jwtConfig;

        public JwtService(JwtSecurityTokenHandler jwtSecurityTokenHandler, JwtOption jwtConfig)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _jwtConfig = jwtConfig;
        }

        /// <summary>
        /// 生成身份信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="roleName">登录时的角色</param>
        /// <returns></returns>
        public Claim[] BuildClaims(string userName, List<string> roleName)
        {
            // 配置用户标识
            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Role,string.Join(',', roleName)),
                new Claim(JwtRegisteredClaimNames.Iss,_jwtConfig.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud,_jwtConfig.Audience),
                new Claim(ClaimTypes.Expiration,_jwtConfig.WebExp.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
            };
            /*
             iss (issuer)：签发人
             exp (expiration time)：过期时间
             sub (subject)：主题
             aud (audience)：受众
             nbf (Not Before)：生效时间
             iat (Issued At)：签发时间
             jti (JWT ID)：编号
             */
            return userClaims;
        }

        /// <summary>
        /// 生成jwt令牌
        /// </summary>
        /// <param name="claims">自定义的claim</param>
        /// <returns></returns>
        public string BuildToken(Claim[] claims)
        {
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecurityKey)), SecurityAlgorithms.HmacSha256); ;
            JwtSecurityToken tokenkey = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.WebExp),
                signingCredentials: creds);
            string tokenStr = "";
            try
            {
                tokenStr = new JwtSecurityTokenHandler().WriteToken(tokenkey);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return tokenStr;
        }

        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public string IssueJwt(TokenModel tokenModel)
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
            double expMin;
            switch (tokenModel.TokenType)
            {
                case TokenTypeEnum.Web:
                    expMin = _jwtConfig.WebExp;
                    break;
                case TokenTypeEnum.App:
                    expMin = _jwtConfig.AppExp;
                    break;
                case TokenTypeEnum.MiniProgram:
                    expMin = _jwtConfig.MiniProgramExp;
                    break;
                default:
                    expMin = _jwtConfig.OtherExp;
                    break;
            }
            DateTime expTime = dateTime.AddMinutes(expMin);

            //秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecurityKey));
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

        /// <summary>
        /// 从Token解密出JwtSecurityToken,JwtSecurityToken : SecurityToken
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public JwtSecurityToken GetJwtSecurityToken(string tokenStr)
        {
            string token = tokenStr.Replace($"{JwtBearerDefaults.AuthenticationScheme} ", string.Empty);
            var jwt = _jwtSecurityTokenHandler.ReadJwtToken(token);
            return jwt;
        }

        /// <summary>
        /// 从 Token 解密出SecurityToken
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public SecurityToken GetSecurityToken(string tokenStr)
        {
            string token = tokenStr.Replace("Bearer ", string.Empty);
            var jwt = _jwtSecurityTokenHandler.ReadToken(token);
            return jwt;
        }
    }
}
