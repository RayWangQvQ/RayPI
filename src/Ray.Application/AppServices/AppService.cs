using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ray.Application.IAppServices;
using Ray.Infrastructure.Guids;
using Ray.Infrastructure.ObjectMapping;

namespace Ray.Application.AppServices
{
    public class AppService : IAppService
    {
        /// <summary>
        /// 当前容器
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        public AppService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IGuidGenerator GuidGenerator => ServiceProvider.GetRequiredService<IGuidGenerator>();

        protected IObjectMapper ObjectMapper => ServiceProvider.GetRequiredService<IObjectMapper>();

        /// <summary>
        /// Checks for given <paramref name="policyName"/>.
        /// Throws <see cref="AuthorizationException"/> if given policy has not been granted.
        /// </summary>
        /// <param name="policyName">The policy name. This method does nothing if given <paramref name="policyName"/> is null or empty.</param>
        protected virtual async Task CheckPolicyAsync(string policyName)
        {
            //todo
        }
    }
}
