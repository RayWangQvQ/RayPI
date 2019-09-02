//微软包
using Microsoft.Extensions.Configuration;

namespace RayPI.Infrastructure.Config.Model
{
    public class AllConfigModel
    {
        private readonly IConfiguration _configuration;

        public AllConfigModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 认证授权配置
        /// </summary>
        public JwtAuthConfigModel JwtAuthConfigModel => new JwtAuthConfigModel(_configuration);

        /// <summary>
        /// 连接字符串配置
        /// </summary>
        public ConnectionStringsModel ConnectionStringsModel => new ConnectionStringsModel(_configuration);
    }
}
