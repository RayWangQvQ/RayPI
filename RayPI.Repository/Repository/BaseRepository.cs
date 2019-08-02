using System;
using System.Collections.Generic;
using System.Linq.Expressions;
//
using SqlSugar;
using RayPI.Model.ReturnModel;


namespace RayPI.SqlSugarRepository.Repository
{
    /// <summary>
    /// 服务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> where T : class, new()
    {
        protected readonly MySqlSugarClient _sugarClient;

        public BaseRepository(MySqlSugarClient sugarClient)
        {
            _sugarClient = sugarClient;
        }

        #region CRUD
        public TableModel<T> GetPageList(int pageIndex, int pageSize)
        {
            PageModel p = new PageModel() { PageIndex = pageIndex, PageSize = pageSize };
            Expression<Func<T, bool>> ex = (it => 1 == 1);
            List<T> data = _sugarClient.SimpleClient.GetPageList(ex, p);
            var t = new TableModel<T>
            {
                Code = 0,
                Count = p.PageCount,
                Data = data,
                Msg = "成功"
            };
            return t;
        }

        public T Get(long id)
        {
            return _sugarClient.SimpleClient.GetById<T>(id);
        }

        public bool Add(T entity)
        {
            return _sugarClient.SimpleClient.Insert(entity);
        }

        public bool Update(T entity)
        {
            return _sugarClient.SimpleClient.Update(entity);
        }

        public bool Dels(dynamic[] ids)
        {
            return _sugarClient.SimpleClient.DeleteByIds<T>(ids);
        }
        #endregion
    }
}
