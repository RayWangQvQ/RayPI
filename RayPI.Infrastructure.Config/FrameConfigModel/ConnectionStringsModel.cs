//微软包

using Microsoft.Extensions.Configuration;

namespace RayPI.Infrastructure.Config.FrameConfigModel
{
    /// <summary>
    /// 连接字符串配置
    /// </summary>
    public class ConnectionStringsModel
    {
        private readonly IConfigurationSection _configSection;

        public ConnectionStringsModel(IConfiguration configuration)
        {
            _configSection = configuration.GetSection("ConnectionStrings");
        }
        public string SqlServerDatabase => _configSection.GetValue("SqlServerDatabase", "");
    }
}
