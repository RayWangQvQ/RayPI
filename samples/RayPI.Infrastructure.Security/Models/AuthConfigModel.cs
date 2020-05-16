using System;

namespace RayPI.Infrastructure.Security.Models
{
    public class AuthConfigModel
    {
        /// <summary>
        /// 加密 Token 的密钥
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public  TimeSpan TimeSpan { get; set; }

        /// <summary>
        /// 权限不足时是否跳转到失败页面
        /// </summary>
        public bool IsDeniedAction { get; set; }

        /// <summary>
        /// 验证失败时跳转到此API
        /// </summary>
        public string DeniedAction { get; set; }

        /// <summary>
        /// 未携带验证信息是否跳转到登录页面
        /// </summary>
        public bool IsLoginAction { get; set; }

        /// <summary>
        /// 未携带任何身份信息时时跳转到登陆API
        /// </summary>
        public string LoginAction { get; set; }

        /// <summary>
        /// 事件传递对象
        /// </summary>
        public AuthenticateScheme scheme { get; set; } = new AuthenticateScheme();

    }
}
