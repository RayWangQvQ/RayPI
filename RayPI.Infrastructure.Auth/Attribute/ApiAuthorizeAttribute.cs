using Microsoft.AspNetCore.Authorization;
using RayPI.Infrastructure.Auth.Enums;

namespace RayPI.Infrastructure.Auth.Attribute
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiAuthorizeAttribute(PolicyEnum policyEnum)
        {
            this.Policy = policyEnum.ToString();
        }
    }
}
