//微软包

using Microsoft.Extensions.Configuration;

namespace RayPI.Infrastructure.Config.ConfigModel
{
    /// <summary>
    /// 连接字符串配置
    /// </summary>
    public class ConnectionStringsConfigModel
    {
        private readonly IConfigurationSection _configSection;

        public ConnectionStringsConfigModel(IConfiguration configuration)
        {
            _configSection = configuration.GetSection("ConnectionStrings");
        }
        public string SqlServerDatabase => _configSection.GetValue("SqlServerDatabase", "");
    }
}
