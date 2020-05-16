//微软包

using Microsoft.AspNetCore.Authorization;
using RayPI.Infrastructure.Security.Enums;

namespace RayPI.Infrastructure.Security
{
    /// <summary>
    /// 授权策略
    /// </summary>
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public PolicyRequirement(OperateEnum operateEnum, ResourceEnum resourceEnum)
        {
            this.OperateEnum = operateEnum;
            this.ResourceEnum = resourceEnum;
        }

        public OperateEnum OperateEnum { get; set; }
        public ResourceEnum ResourceEnum { get; set; }
    }
}
