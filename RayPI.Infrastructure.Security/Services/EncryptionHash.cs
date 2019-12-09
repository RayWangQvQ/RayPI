using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RayPI.Infrastructure.Security.Models;

namespace RayPI.Infrastructure.Security.Services
{
    /// <summary>
    /// 用于加密生成
    /// </summary>
    public class EncryptionHash
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        public EncryptionHash()
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// 获取字符串的哈希值
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="clearChar">需要去除的特殊字符</param>
        /// <returns></returns>
        public static string GetByHashString(string source, char[] clearChar = null)
        {
            if (clearChar != null)
            {
                foreach (var item in clearChar)
                {
                    source = source.Replace(item.ToString(), String.Empty);
                }
            }

            string hash = GetByHashString(source);
            return hash;
        }

        /// <summary>
        /// 将字符串生成密钥
        /// </summary>
        /// <param name="key">字符串</param>
        /// <param name="encryptionType">加密方式</param>
        /// <returns></returns>
        public SigningCredentials GetTokenSecurityKey(string key, string encryptionType = SecurityAlgorithms.HmacSha256)
        {
            var securityKey = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key)), encryptionType);
            return securityKey;
        }


        /// <summary>
        /// 获取字符串的哈希值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string GetByHashString(string source)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            md5Hash.Dispose();
            return sBuilder.ToString();
        }


        /// <summary>
        /// 生成jwt令牌
        /// </summary>
        /// <param name="claims">自定义的claim</param>
        /// <returns></returns>
        public ResponseToken BuildToken(Claim[] claims)
        {

            SigningCredentials creds = AuthConfig.SigningCredentials;
            JwtSecurityToken tokenkey = new JwtSecurityToken(
                issuer: AuthConfig.model.Issuer,
                audience: AuthConfig.model.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(AuthConfig.model.TimeSpan.TotalMinutes),
                signingCredentials: creds);
            string tokenstr = "";
            try
            {
                tokenstr = new JwtSecurityTokenHandler().WriteToken(tokenkey);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new ResponseToken
            {
                Status = true,
                Access_Token = tokenstr,
                Token_Type = JwtBearerDefaults.AuthenticationScheme,
                Expires_In = AuthConfig.model.TimeSpan.TotalSeconds
            };
        }

        /// <summary>
        /// 生成 JwtSecurityToken
        /// </summary>
        /// <param name="claims">自定义的claim</param>
        /// <returns>JwtSecurityToken</returns>
        public JwtSecurityToken BuildJwtToken(Claim[] claims)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthConfig.model.Issuer,
                audience: AuthConfig.model.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(AuthConfig.model.TimeSpan),
                signingCredentials: AuthConfig.SigningCredentials
            );

            return jwt;
        }


        /// <summary>
        /// 生成 Token 信息
        /// </summary>
        /// <param name="jwt">JWT 令牌</param>
        /// <param name="timeSpan">Token过期时间</param>
        /// <returns>匿名类型</returns>
        public dynamic BuildJwtTokenDynamic(JwtSecurityToken jwt)
        {
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                status = true,
                access_token = encodedJwt,
                expires_in = AuthConfig.model.TimeSpan.TotalMilliseconds,
                token_type = "Bearer"
            };
            return response;
        }

        /// <summary>
        /// 生成 Token 信息
        /// </summary>
        /// <param name="jwt">JWT 令牌</param>
        /// <param name="timeSpan">Token过期时间</param>
        /// <returns>CZGL.Auth.Models。ResponseToken</returns>
        public ResponseToken BuildJwtResponseToken(JwtSecurityToken jwt)
        {
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new ResponseToken
            {
                Status = true,
                Access_Token = encodedJwt,
                Expires_In = AuthConfig.model.TimeSpan.TotalSeconds,
                Token_Type = "Bearer"
            };
            return response;
        }


        /// <summary>
        /// 直接生成 Token
        /// </summary>
        /// <param name="jwt">JWT 令牌</param>
        /// <param name="timeSpan">Token过期时间</param>
        /// <returns>Token字符串</returns>
        public string BuildJwtToken(JwtSecurityToken jwt)
        {
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        /// <summary>
        /// 生成身份信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="roleName">登录时的角色</param>
        /// <param name="audience">订阅者</param>
        /// <returns></returns>
        public Claim[] BuildClaims(string userName, string roleName)
        {
            // 配置用户标识
            var userClaims = new Claim[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Role,roleName),
                //new Claim(JwtRegisteredClaimNames.Aud,AuthConfig.model.Audience),
                //new Claim(ClaimTypes.Expiration,AuthConfig.model.TimeSpan.TotalSeconds.ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
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
        /// 生成用户标识
        /// </summary>
        public ClaimsIdentity GetIdentity(Claim[] claims)
        {
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaims(claims);
            return identity;
        }

        /// <summary>
        /// Token是否是符合要求的标准 Json Web 令牌
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public bool IsCanReadToken(string tokenStr)
        {
            if (!tokenStr.Contains(JwtBearerDefaults.AuthenticationScheme)) return false;

            tokenStr = tokenStr.Replace($"{JwtBearerDefaults.AuthenticationScheme} ", string.Empty);
            bool isCan = _jwtSecurityTokenHandler.CanReadToken(tokenStr);

            return isCan;
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
        public IEnumerable<Claim> GetClaims(JwtSecurityToken jwt)
        {
            var claims = jwt.Claims;
            return claims;
        }
    }
}
