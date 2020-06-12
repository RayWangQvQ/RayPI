using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Ray.Infrastructure.Security.Claims;
using Ray.Infrastructure.Security.Principal;

namespace Ray.Infrastructure.Security.User
{
    public class CurrentUser : ICurrentUser
    {
        private static readonly Claim[] EmptyClaimsArray = new Claim[0];

        public bool IsAuthenticated => Id.HasValue;

        public Guid? Id => _principalAccessor.Principal?.FindUserId();

        public string UserName => this.FindClaimValue(RayClaimTypes.UserName);

        public string PhoneNumber => this.FindClaimValue(RayClaimTypes.PhoneNumber);

        public bool PhoneNumberVerified => string.Equals(this.FindClaimValue(RayClaimTypes.PhoneNumberVerified), "true", StringComparison.InvariantCultureIgnoreCase);

        public string Email => this.FindClaimValue(RayClaimTypes.Email);

        public bool EmailVerified => string.Equals(this.FindClaimValue(RayClaimTypes.EmailVerified), "true", StringComparison.InvariantCultureIgnoreCase);

        public Guid? TenantId { get; }

        public string[] Roles => FindClaims(RayClaimTypes.Role).Select(c => c.Value).ToArray();

        private readonly ICurrentPrincipalAccessor _principalAccessor;

        public CurrentUser(ICurrentPrincipalAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }

        public Claim FindClaim(string claimType)
        {
            return _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == claimType);
        }

        public Claim[] FindClaims(string claimType)
        {
            return _principalAccessor.Principal?.Claims.ToArray() ?? EmptyClaimsArray;
        }

        public Claim[] GetAllClaims()
        {
            return _principalAccessor.Principal?.Claims.ToArray() ?? EmptyClaimsArray;
        }

        public bool IsInRole(string roleName)
        {
            return FindClaims(RayClaimTypes.Role).Any(c => c.Value == roleName);
        }
    }
}
