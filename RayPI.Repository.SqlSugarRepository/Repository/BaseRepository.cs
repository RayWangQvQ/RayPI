using System;
using System.Collections.Generic;
using System.Linq.Expressions;
//
using SqlSugar;
using System.Linq;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Treasury.Enums;
using RayPI.Infrastructure.Treasury.Models;

namespace RayPI.SqlSugarRepository.Repository
{
    /// <summary>
    /// 服务层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityBase, new()
    {
        protected readonly MySqlSugarClient _sugarClient;

        public BaseRepository(MySqlSugarClient sugarClient)
        {
            _sugarClient = sugarClient;
        }

        public IQueryable<T> GetAllMatching(Expression<Func<T, bool>> filter = null, bool exceptDeleted = true)
        {
            throw new NotImplementedException();
        }

        public PageResult<T> GetPageList<TK>(int pageIndex, int pageSize, bool exceptDeleted, Expression<Func<T, bool>> filterExpression, Expression<Func<T, TK>> orderByExpression, SortEnum sortOrder)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<T, bool>> filter, bool exceptDeleted)
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> filter, bool exceptDeleted)
        {
            throw new NotImplementedException();
        }

        public T FindById(long id, bool exceptDeleted)
        {
            throw new NotImplementedException();
        }

        long IBaseRepository<T>.Add(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<long> Add(IEnumerable<T> entityList)
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<T>.Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IQueryable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(IQueryable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IQueryable<T> entityList)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
