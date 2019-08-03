using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//
using Microsoft.EntityFrameworkCore;
//
using Ray.EntityFrameworkRepository;
using RayPI.Entity;
using RayPI.IRepository;
using RayPI.Treasury.Enums;
using RayPI.Treasury.Models;


namespace RayPI.EntityFrameworkRepository.Repository
{
    /// <summary>
    /// 仓储层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly MyDbContext _myDbContext;

        public BaseRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
            _dbSet = myDbContext.Set<T>();
        }

        #region 查询
        /// <summary>查询所有匹配项</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="exceptDeleted">排除被逻辑删除的</param>
        /// <returns>IQueryable<T></T></returns>
        public virtual IQueryable<T> GetAllMatching(Expression<Func<T, bool>> filter = null, bool exceptDeleted = true)
        {
            IQueryable<T> source;
            if (filter == null)
                source = _dbSet.Where(x => true);
            else
                source = _dbSet.Where(filter);
            if (exceptDeleted)
                source = source.Where(x => x.IsDeleted == false);
            return source;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="exceptDeleted"></param>
        /// <param name="filterExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual PageResult<T> GetPageList<TK>(int pageIndex, int pageSize,
            bool exceptDeleted = true, Expression<Func<T, bool>> filterExpression = null,
            Expression<Func<T, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc)
        {
            IQueryable<T> source = GetAllMatching(filterExpression, exceptDeleted);
            int preCount = pageSize * (pageIndex - 1);
            int totalCount = source.Count();

            //排序
            IQueryable<T> sourceOrder;
            switch (sortOrder)
            {
                case SortEnum.Asc:
                    //排序
                    if (orderByExpression == null)//默认根据Id排序
                        sourceOrder = source.OrderBy(x => x.Id);
                    else
                        sourceOrder = source.OrderBy(orderByExpression);
                    break;
                case SortEnum.Desc:
                    //排序
                    if (orderByExpression == null)
                        sourceOrder = source.OrderByDescending(x => x.Id);
                    else
                        sourceOrder = source.OrderByDescending(orderByExpression);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }
            //分页
            IQueryable<T> sourcePage = sourceOrder.Skip(preCount).Take(pageSize);
            int totalPages = totalCount > 0
                ? (int)Math.Ceiling((double)totalCount / (double)pageSize)
                : 0;
            return new PageResult<T>()
            {
                List = sourcePage.ToList(),
                PageIndex = totalPages <= 0
                    ? 1
                    : (pageIndex > totalPages
                        ? totalPages
                        : pageIndex),
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }

        /// <summary>判断是否存在</summary>
        /// <param name="filter">查询条件</param>
        /// <param name="exceptDeleted">是否忽略已逻辑删除的数据</param>
        /// <returns>true=存在，false=不存在</returns>
        public virtual bool Any(Expression<Func<T, bool>> filter, bool exceptDeleted = true)
        {
            return GetAllMatching(filter, exceptDeleted).Any();
        }

        /// <summary>根据条件获取</summary>
        /// <param name="filter">查询条件</param>
        /// <returns>T.</returns>
        public virtual T Find(Expression<Func<T, bool>> filter, bool exceptDeleted = true)
        {
            IQueryable<T> source = GetAllMatching(filter, exceptDeleted);
            return source.FirstOrDefault();
        }

        /// <summary>
        /// 根据Id获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exceptDeleted"></param>
        /// <returns></returns>
        public virtual T FindById(long id, bool exceptDeleted = true)
        {
            return Find(x => x.Id == id, exceptDeleted);
        }
        #endregion

        #region 添加
        public long Add(T entity)
        {
            //todo:设置操作人信息
            _dbSet.Add(entity);
            _myDbContext.SaveChanges();
            return entity.Id;
        }
        /// <summary>批量新增</summary>
        /// <param name="tAggregateRoots">实体集合</param>
        /// <returns>IEnumerable&lt;System.Int64&gt;.</returns>
        public virtual IEnumerable<long> Add(IEnumerable<T> entityList)
        {
            //todo
            _dbSet.AddRange(entityList);
            _myDbContext.SaveChanges();
            return entityList.Select(x => x.Id);
        }
        #endregion

        #region 更新
        /// <summary>更新实体</summary>
        /// <param name="item">The item.</param>
        /// <param name="ignoreFileds">忽略部分字段更新</param>        /// <exception cref="T:System.NotImplementedException"></exception>
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
            _myDbContext.SaveChanges();
        }

        /// <summary>批量修改</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public virtual void Update(IQueryable<T> entityList)
        {
            _dbSet.UpdateRange(entityList);
            _myDbContext.SaveChanges();
        }
        #endregion

        #region 删除
        #region 物理移除
        /// <summary>物理移除</summary>
        /// <param name="item">实体</param>
        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
            _myDbContext.SaveChanges();
        }

        /// <summary>批量物理移除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public void Remove(IQueryable<T> entityList)
        {
            _dbSet.RemoveRange(entityList);
            _myDbContext.SaveChanges();
        }

        /// <summary>批量物理移除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void Remove(Expression<Func<T, bool>> filter)
        {
            _dbSet.Remove(Find(filter));
            _myDbContext.SaveChanges();
        }
        /// <summary>物理移除</summary>
        /// <param name="id">主键</param>
        public virtual void Remove(long id)
        {
            Remove(x => x.Id == id);
            _myDbContext.SaveChanges();
        }
        #endregion

        #region 逻辑删除(实质是更新：更新实体的IsDeleted字段和DeleteTime字段)
        /// <summary>逻辑删除</summary>
        /// <param name="item">The item.</param>
        public virtual void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
            _myDbContext.SaveChanges();
        }

        /// <summary>逻辑删除</summary>
        /// <param name="tAggregateRoots">批量实体</param>
        public void Delete(IQueryable<T> entityList)
        {
            foreach (var entity in entityList)
            {
                entity.IsDeleted = true;
            }
            Update(entityList);
            _myDbContext.SaveChanges();
        }

        /// <summary>逻辑删除</summary>
        /// <param name="filter">移除条件</param>
        public virtual void Delete(Expression<Func<T, bool>> filter)
        {
            var entityList = GetAllMatching(filter);
            Delete(entityList);
            _myDbContext.SaveChanges();
        }
        /// <summary>逻辑删除</summary>
        /// <param name="id">The item.</param>
        public virtual void Delete(long id)
        {
            Delete(x => x.Id == id);
            _myDbContext.SaveChanges();
        }
        #endregion
        #endregion
    }
}
