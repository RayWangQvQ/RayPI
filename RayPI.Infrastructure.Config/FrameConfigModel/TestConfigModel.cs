using Microsoft.Extensions.Configuration;

namespace RayPI.Infrastructure.Config.FrameConfigModel
{
    public class TestConfigModel
    {
        private readonly IConfigurationSection _config;

        public TestConfigModel(IConfiguration configuration)
        {
            _config = configuration.GetSection("Test");
        }

        public string Key1 => _config.GetValue("Key1", "");
        public string Key2 => _config.GetValue("Key2", "");
    }
}
