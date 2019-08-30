//微软包
using Microsoft.Extensions.Configuration;

namespace RayPI.Infrastructure.Config.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtAuthConfigModel
    {
        private readonly IConfigurationSection _configSection;

        public JwtAuthConfigModel(IConfiguration configuration)
        {
            _configSection = configuration.GetSection("JwtAuth");
        }

        /// <summary>
        /// 安全密钥
        /// </summary>
        public string SecurityKey => _configSection.GetValue("SecurityKey", "123456");

        /// <summary>
        /// Web端过期时间
        /// </summary>
        public double WebExp => _configSection.GetValue<double>("WebExp", 30);

        /// <summary>
        /// 移动端过期时间
        /// </summary>
        public double AppExp => _configSection.GetValue<double>("AppExp", 30);

        /// <summary>
        /// 小程序过期时间
        /// </summary>
        public double MiniProgramExp => _configSection.GetValue<double>("MiniProgramExp", 30);

        /// <summary>
        /// 其他端过期时间
        /// </summary>
        public double OtherExp => _configSection.GetValue<double>("OtherExp", 30);
    }
}
