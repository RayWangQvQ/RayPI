using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RayPI.AuthService
{
    public class PolicyRequirement : IAuthorizationRequirement
    {
        public PolicyRequirement(string role)
        {
            this.RequireRole = role.Split(',');
        }

        public string[] RequireRole { get; set; }
    }
}
