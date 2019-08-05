using RayPI.IRepository;
using RayPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Bussiness.System
{
    public class EntityBLL
    {
        private IEntityRepository _entityRepository;

        public EntityBLL(IEntityRepository entityRepository)
        {

        }

        public bool CreateEntity(string entityName, string contentRootPath)
        {
            string[] arr = contentRootPath.Split('\\');
            string baseFileProvider = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                baseFileProvider += arr[i];
                baseFileProvider += "\\";
            }
            string filePath = baseFileProvider + "RayPI.Entity";
            return _entityRepository.CreateEntity(entityName, filePath);
        }
    }
}
