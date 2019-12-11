using RayPI.Infrastructure.Auth.Enums;
using System;

namespace RayPI.Infrastructure.Auth
{
    public class Permission
    {
        public Permission(string operateCode, string resourceCode)
        {
            OperateCode = operateCode;
            ResourceCode = resourceCode;
        }

        public OperateEnum OperateEnum => Enum.Parse<OperateEnum>(OperateCode);
        public ResourceEnum ResourceEnum => Enum.Parse<ResourceEnum>(ResourceCode);
        public string OperateCode { get; set; }
        public string ResourceCode { get; set; }

    }
}
