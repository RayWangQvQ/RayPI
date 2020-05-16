//微软包
using Microsoft.Extensions.Configuration;

namespace RayPI.Infrastructure.Auth.Jwt
{
    /// <summary>
    /// Jwt配置
    /// </summary>
    public class JwtOption
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 受众
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 安全密钥
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// Web端过期时间
        /// </summary>
        public double WebExp { get; set; }

        /// <summary>
        /// 移动端过期时间
        /// </summary>
        public double AppExp { get; set; }

        /// <summary>
        /// 小程序过期时间
        /// </summary>
        public double MiniProgramExp { get; set; }

        /// <summary>
        /// 其他端过期时间
        /// </summary>
        public double OtherExp { get; set; }
    }
}
