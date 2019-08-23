using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;
using System.Xml;

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
            /*
            swaggerDoc.Tags = new List<Tag>
            {
                //添加对应的控制器描述 这个是我好不容易在issues里面翻到的
                new Tag { Name = "Admin", Description = "后台" },
                new Tag { Name = "Client", Description = "客户端" },
                new Tag { Name = "System", Description = "系统" }
            };
            swaggerDoc.Tags = GetControllerDesc();
            */
        }

        /// <summary>
        /// 从xml注释中读取控制器注释
        /// </summary>
        /// <returns></returns>
        private List<Tag> GetControllerDesc()
        {
            List<Tag> tagList = new List<Tag>();

            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var xmlpath = Path.Combine(basePath, "APIHelp.xml");
            if (!File.Exists(xmlpath))//检查xml注释文件是否存在
                return tagList;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);

            string memberName = string.Empty;//xml三级节点的name属性值
            string controllerName = string.Empty;//控制器完整名称
            string key = string.Empty;//控制器去Controller名称
            string value = string.Empty;//控制器注释

            foreach (XmlNode node in xmlDoc.SelectNodes("//member"))//循环三级节点member
            {
                memberName = node.Attributes["name"].Value;
                if (memberName.StartsWith("T:"))//T:开头的代表类
                {
                    string[] arrPath = memberName.Split('.');
                    controllerName = arrPath[arrPath.Length - 1];
                    if (controllerName.EndsWith("Controller"))//Controller结尾的代表控制器
                    {
                        XmlNode summaryNode = node.SelectSingleNode("summary");//注释节点
                        key = controllerName.Remove(controllerName.Length - "Controller".Length, "Controller".Length);
                        if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !tagList.Contains(new Tag { Name = key }))
                        {
                            value = summaryNode.InnerText.Trim();
                            tagList.Add(new Tag { Name = key, Description = value });
                        }
                    }
                }
            }
            return tagList;
        }
    }
}