using RayPI.IService;
using RayPI.Model;
using SqlSugar;
using System;

namespace RayPI.Service
{
    /// <summary>
    /// 实体操作服务
    /// </summary>
    public class EntityService : BaseDB, IEntity
    {
        public SqlSugarClient db = GetClient();
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool CreateEntity(string entityName, string filePath)
        {
            try
            {
                db.DbFirst
                    .IsCreateAttribute()
                    .Where(entityName)
                    .CreateClassFile(filePath,"RayPI.Entity");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
