using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using RayPI.AuthService.Enums;
using RayPI.Treasury.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.AuthService
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiAuthorizeAttribute(PolicyEnum policyEnum)
        {
            this.Policy = policyEnum.ToString();
        }
    }
}
