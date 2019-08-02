using System;
using System.Linq.Expressions;
//
using Ray.EntityFrameworkRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RayPI.Entity;
using RayPI.Treasury.Models;

namespace RayPI.EntityFrameworkRepository.Repository
{
    /// <summary>
    /// 仓储层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly MyDbContext _myDbContext;

        public BaseRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
            _dbSet = myDbContext.Set<T>();
        }
        #region 查询
        /// <summary>查询实体列表</summary>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        public virtual IQueryable<T> GetAll()
        {
            return this.GetAllMatching(x => true);
        }

        /// <summary>不分页查询</summary>
        /// <param name="specification">查询条件</param>
        /// <returns>IQueryable&lt;TAggregateRoot&gt;.</returns>
        public virtual IQueryable<T> GetAllMatching(Expression<Func<T, bool>> ex)
        {
            IQueryable<T> source = _dbSet.Where(ex);
            return source;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public PageResult<T> GetPageList<TK>(int pageIndex, int pageSize, Expression<Func<T, bool>> filterExpression, Expression<Func<T, TK>> orderByExpression = null,)
        {
            IQueryable<T> source1 = GetAllMatching(x => true);
            int count1 = pageSize * (pageIndex - 1);
            int count2 = pageSize;
            int num1 = source1.Count<T>(filterExpression);
            IQueryable<T> source2 = source1.Where<T>(filterExpression);
            IOrderedQueryable<T> source3;
            if (orderByExpression == null)
                source3 = source2.OrderBy(x => x.Id);
            else
                source3 = source2.OrderBy(orderByExpression);
            IQueryable<T> source4 = source3.Skip(count1).Take<T>(count2);
            int num2 = num1 > 0 ? (int)Math.Ceiling((double)num1 / (double)pageSize) : 0;
            return new PageResult<T>()
            {
                List = source4.ToList<T>(),
                PageIndex = num2 <= 0 ? 1 : (pageIndex > num2 ? num2 : pageIndex),
                PageSize = pageSize,
                TotalCount = num1,
                TotalPages = num2
            };
        }

        /// <summary>根据条件获取聚合根信息</summary>
        /// <param name="filter">查询条件</param>
        /// <returns>T.</returns>
        public virtual T Find(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> source = _dbSet.Where(filter);
            return source.FirstOrDefault();
        }

        public T Get(long id)
        {
            return Find(x => x.Id == id);
        }
        #endregion

        #region 添加
        public bool Add(T entity)
        {
            _dbSet.Add(entity);
            _myDbContext.SaveChanges();
            return true;
        }
        #endregion

        #region 更新
        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            _myDbContext.SaveChanges();
            return true;
        }
        #endregion

        #region 删除
        #region 物理移除
        /// <summary>物理删除一个对象</summary>
        /// <typeparam name="TAggregateRoot">The type of the t aggregate root.</typeparam>
        /// <param name="item">The item.</param>
        public void Remove(T item)
        {
            if (item == null) return;
            _dbSet.Remove(item);
        }

        /// <summary>批量物理移除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void RemoveAllMatching(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> list = GetAllMatching(filter);
            foreach (var item in list)
            {
                _dbSet.Remove(item);
            }
            _myDbContext.SaveChanges();
        }

        public void Remove(IQueryable<T> list)
        {
            foreach (var item in list)
            {
                _dbSet.Remove(item);
            }
            _myDbContext.SaveChanges();
        }
        #endregion

        #region 逻辑删除(实质是更新：更新实体的IsDeleted字段和DeleteTime字段)

        #endregion
        #endregion
    }
}
