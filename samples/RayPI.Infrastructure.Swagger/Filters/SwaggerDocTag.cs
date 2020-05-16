//系统包
using System.Collections.Generic;
using System.IO;
using System.Xml;
//微软包
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
//三方包
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace RayPI.Infrastructure.Swagger.Filters
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
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = GetControllerDesc();
        }

        /// <summary>
        /// 从xml注释中读取控制器注释
        /// </summary>
        /// <returns></returns>
        private List<OpenApiTag> GetControllerDesc()
        {
            var tagList = new List<OpenApiTag>();

            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var xmlpath = Path.Combine(basePath, "APIHelp.xml");
            if (!File.Exists(xmlpath))//检查xml注释文件是否存在
                return tagList;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);

            foreach (XmlNode node in xmlDoc.SelectNodes("//member"))//循环三级节点member
            {
                var memberName = node.Attributes["name"].Value;//xml三级节点的name属性值
                if (memberName.StartsWith("T:"))//T:开头的代表类
                {
                    string[] arrPath = memberName.Split('.');
                    var controllerName = arrPath[^1];//控制器完整名称
                    if (controllerName.EndsWith("Controller"))//Controller结尾的代表控制器
                    {
                        XmlNode summaryNode = node.SelectSingleNode("summary");//注释节点
                        var key = controllerName.Remove(controllerName.Length - "Controller".Length, "Controller".Length);//控制器去Controller名称
                        if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !tagList.Contains(new OpenApiTag { Name = key }))
                        {
                            var value = summaryNode.InnerText.Trim();//控制器注释
                            tagList.Add(new OpenApiTag { Name = key, Description = value });
                        }
                    }
                }
            }
            return tagList;
        }
    }
}