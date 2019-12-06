using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Infrastructure.Security.Enums;

namespace RayPI.Infrastructure.Security
{
    public class Permission
    {
        public OperateEnum OperateEnum { get; set; }
        public ResourceEnum ResourceEnum { get; set; }
    }
}
