using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RayPI.Infrastructure.Auth.Authorize
{
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public PolicyRequirement(string role)
        {
            this.RequireRoles = role.Split(',');
        }

        public string[] RequireRoles { get; set; }
    }
}
