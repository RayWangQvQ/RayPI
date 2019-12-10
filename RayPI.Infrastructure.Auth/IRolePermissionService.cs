using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Auth
{
    public interface IRolePermissionService
    {
        List<Permission> GetPermissions(string roleCode);
    }
}
