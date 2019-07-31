using RayPI.IRepository;
using RayPI.Repository.Base;
using System;

namespace RayPI.Repository
{
    /// <summary>
    /// 实体操作服务
    /// </summary>
    public class EntityRepository : BaseRepository<object>, IEntityRepository
    {
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
                    .CreateClassFile(filePath, "RayPI.Entity");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
