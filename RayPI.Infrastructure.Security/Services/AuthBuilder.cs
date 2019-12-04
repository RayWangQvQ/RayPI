using System;
using RayPI.Infrastructure.Security.Models;

namespace RayPI.Infrastructure.Security.Services
{
    public class AuthBuilder
    {
        private readonly AuthConfigModel authModel;
        public AuthBuilder()
        {
            authModel = new AuthConfigModel();
        }

        /// <summary>
        /// 配置颁发认证
        /// </summary>
        /// <param name="securityKey">密钥</param>
        /// <param name="issuer">颁发者</param>
        /// <param name="audience">订阅者</param>
        /// <returns></returns>
        public AuthBuilder Security(string securityKey = "MIICdwIBADANBg",
            string issuer = "ASPNETCORE",
            string audience = "ASPNETCORE")
        {
            authModel.SecurityKey = securityKey;
            authModel.Issuer = issuer;
            authModel.Audience = audience;

            return this;
        }

        /// <summary>
        /// Jwt过期时间
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public AuthBuilder Time(TimeSpan timeSpan)
        {
            authModel.TimeSpan = timeSpan;

            return this;
        }

        /// <summary>
        /// 配置特殊URL
        /// </summary>
        /// <param name="loginApi">跳转到登陆URL</param>
        /// <param name="logoutApi">注销URL</param>
        /// <param name="deniedApi">验证失败跳转URL</param>
        /// <param name="isLogin">是否开启跳转登陆</param>
        /// <param name="isDenied">验证失败是是否跳转</param>
        /// <returns></returns>
        public AuthBuilder Jump(string loginApi = "/account/login",
            string deniedApi = "/account/error",
            bool isLogin = false,
            bool isDenied = false
            )
        {
            authModel.LoginAction = loginApi;
            authModel.DeniedAction = deniedApi;
            authModel.IsLoginAction = isLogin;
            authModel.IsDeniedAction = isDenied;

            return this;
        }

        public AuthBuilder InfoScheme(AuthenticateScheme scheme)
        {
            if (scheme == null)
                return this;

            authModel.scheme = scheme;
            return this;
        }
        /// <summary>
        /// 构建配置
        /// </summary>
        /// <returns></returns>
        public AuthConfigModel Build()
        {
            return authModel;
        }
    }
}
