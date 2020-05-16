using Microsoft.AspNetCore.Cors;
using RayPI.Infrastructure.Cors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Cors.Attributes
{
    public class RayCorsAttribute : EnableCorsAttribute
    {
        public RayCorsAttribute(CorsPolicyEnum policyEnum) : base(policyEnum.ToString())
        {

        }
    }
}
