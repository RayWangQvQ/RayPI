using RayPI.IRepository;
using RayPI.Model;
using RayPI.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Bussiness.System
{
    public class EntityBLL
    {
        private IEntityRepository iDAL = new EntityRepository();

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
            return iDAL.CreateEntity(entityName, filePath);
        }
    }
}
