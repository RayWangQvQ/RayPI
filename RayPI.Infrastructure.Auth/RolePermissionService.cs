using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Infrastructure.Auth.Enums;

namespace RayPI.Infrastructure.Auth
{
    public class RolePermissionService : IRolePermissionService
    {
        public List<Permission> GetPermissions(string roleCode)
        {
            return new List<Permission>
            {
                new Permission(OperateEnum.Retrieve.ToString(), ResourceEnum.Student.ToString())
            };
        }
    }
}
