using System;
using System.Security.Claims;

namespace Ray.Infrastructure.Security.User
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        Guid? Id { get; }

        string UserName { get; }

        string PhoneNumber { get; }

        bool PhoneNumberVerified { get; }

        string Email { get; }

        bool EmailVerified { get; }

        Guid? TenantId { get; }

        string[] Roles { get; }

        Claim FindClaim(string claimType);

        Claim[] FindClaims(string claimType);

        Claim[] GetAllClaims();

        bool IsInRole(string roleName);
    }
}
