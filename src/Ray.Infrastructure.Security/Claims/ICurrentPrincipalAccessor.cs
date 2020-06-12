using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Ray.Infrastructure.Security.Claims
{
    public interface ICurrentPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }

        IDisposable Change(ClaimsPrincipal principal);
    }
}
