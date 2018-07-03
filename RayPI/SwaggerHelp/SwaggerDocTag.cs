using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayPI.SwaggerHelp
{
    /// <summary>
    /// Swagger注释帮助类
    /// </summary>
    public class SwaggerDocTag : IDocumentFilter
    {
        /// <summary>
        /// 添加附加注释
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<Tag>
            {
                //添加对应的控制器描述 这个是我好不容易在issues里面翻到的
                new Tag { Name = "Admin", Description = "后台" },
                new Tag { Name = "Client", Description = "客户端" },
                new Tag { Name = "System", Description = "系统" }
            };
        }
    }
}