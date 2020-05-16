//微软包
using Microsoft.Extensions.Configuration;
using RayPI.Infrastructure.Config.ConfigModel;
//本地项目包

namespace RayPI.Infrastructure.Config
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
        public ConnectionStringsConfigModel ConnectionStringsModel => new ConnectionStringsConfigModel(_configuration);

        public TestConfigModel TestConfigModel => new TestConfigModel(_configuration);


        public RayPIConfigModel RayPIConfigModel => new RayPIConfigModel(_configuration);
    }
}
