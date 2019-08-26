using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Domain.IRepository;

namespace RayPI.Bussiness.System
{
    public class EntityBusiness
    {
        private IEntityRepository _entityRepository;

        public EntityBusiness()
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
