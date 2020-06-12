using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Ray.Infrastructure.Helpers;
using Ray.Infrastructure.Security.Claims;

namespace Ray.Infrastructure.Security.Principal
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
