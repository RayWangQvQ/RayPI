using RayPI.IService;
using RayPI.Model;
using RayPI.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Bussiness.System
{
    public class EntityBLL
    {
        private IEntity iService = new EntityService();

        public MessageModel<string> CreateEntity(string entityName, string contentRootPath)
        {
            string[] arr = contentRootPath.Split('\\');
            string baseFileProvider = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                baseFileProvider += arr[i];
                baseFileProvider += "\\";
            }
            string filePath = baseFileProvider + "RayPI.Entity";
            if (iService.CreateEntity(entityName, filePath))
                return new MessageModel<string> { Success = true, Msg = "生成成功" };
            else
                return new MessageModel<string> { Success = false, Msg = "生成失败" };
        }
    }
}
