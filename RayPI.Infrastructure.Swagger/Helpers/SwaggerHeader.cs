//系统包
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
//三方包
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RayPI.Infrastructure.Swagger.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerHeader : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

        }
    }
}
