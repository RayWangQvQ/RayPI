
//微软包

using Microsoft.Extensions.Configuration;

namespace RayPI.Infrastructure.Config.ConfigModel
{
    public class RayPIConfigModel
    {
        private readonly IConfiguration _configuration;

        public RayPIConfigModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
