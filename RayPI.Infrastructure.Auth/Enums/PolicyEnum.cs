using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.AuthService.Enums
{
    public enum PolicyEnum
    {
        RequireRoleOfClient,
        RequireRoleOfAdmin,
        RequireRoleOfAdminOrClient
    }
}
