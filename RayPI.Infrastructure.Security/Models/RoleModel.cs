using System.Collections.Generic;

namespace RayPI.Infrastructure.Security.Models
{
    public class RoleModel
    {
        public string RoleName { get; set; }
        public List<ApiInfoModel> Apis { get; set; }
    }
}
