using System;
using System.Diagnostics;

namespace Ray.Security.User
{
    public static class CurrentUserExtensions
    {
        public static string FindClaimValue(this ICurrentUser currentUser, string claimType)
        {
            return currentUser.FindClaim(claimType)?.Value;
        }

        public static T FindClaimValue<T>(this ICurrentUser currentUser, string claimType)
            where T : struct
        {
            var value = currentUser.FindClaimValue(claimType);
            if (value == null)
            {
                return default;
            }

            return value.To<T>();
        }

        public static Guid GetId(this ICurrentUser currentUser)
        {
            Debug.Assert(currentUser.Id != null, "currentUser.Id != null");

            return currentUser.Id.Value;
        }
    }
}
