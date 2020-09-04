using System;
using System.Linq;
using System.Security.Claims;
using Ray.Infrastructure.Helpers;
using Ray.Security.Claims;

namespace Ray.Security.Principal
{
    public static class RayClaimsIdentityExtension
    {
        public static Guid? FindUserId(this ClaimsPrincipal principal)
        {
            CheckHelper.NotNull(principal, nameof(principal));

            var userIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == RayClaimTypes.UserId);
            if (userIdOrNull == null || userIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }
            if (Guid.TryParse(userIdOrNull.Value, out Guid result))
            {
                return result;
            }
            return null;
        }
    }
}
