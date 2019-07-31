using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Repository.Base
{
    public class SqlSugarDbContext
    {
        private readonly SqlSugarClient _sqlSugarClient;

        public SqlSugarDbContext()
        {
            var connectionConfig = new ConnectionConfig
            {
                ConnectionString = "server=.;uid=sa;pwd=Admin,123;database=RayPI",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            };
            _sqlSugarClient = new SqlSugarClient(connectionConfig);
        }
    }
}
