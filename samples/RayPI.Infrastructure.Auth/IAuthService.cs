using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Auth
{
    public interface IAuthService
    {
        string GetToken(string userName, List<string> roleCodeList);
        List<Permission> GetPermissions(List<string> roleCodeList);
    }
}
