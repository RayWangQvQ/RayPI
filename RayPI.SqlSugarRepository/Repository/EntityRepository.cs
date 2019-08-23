using System;
using RayPI.Domain.IRepository;

namespace RayPI.SqlSugarRepository.Repository
{
    /// <summary>
    /// 实体操作服务
    /// </summary>
    public class EntityRepository : IEntityRepository
    {
        private readonly MySqlSugarClient _sugarClient;
        public EntityRepository(MySqlSugarClient sugarClient)
        {
            _sugarClient = sugarClient;
        }

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
                _sugarClient.Client.DbFirst
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
