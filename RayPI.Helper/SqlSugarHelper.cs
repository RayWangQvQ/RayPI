using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayPI.Helper
{
    public static class SqlSugarHelper
    {
        /// <summary>
        /// 获取SqlSugar
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetClient()
        {
            SqlSugarClient db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = "server=.;uid=sa;pwd=Admin,123;database=RayPI",
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true
                }
            );
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
            return db;
        }
    }
}
