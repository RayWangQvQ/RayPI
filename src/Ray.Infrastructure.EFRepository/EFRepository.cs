using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ray.Domain;
using Ray.Domain.RepositoryInterfaces;
using Ray.Infrastructure.Extensions;
using Ray.Infrastructure.Extensions.Linq;
using Ray.Infrastructure.Helpers.Page;

namespace Ray.Infrastructure.EFRepository
{
    /// <summary>
    /// EF仓储抽象基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEFContext"></typeparam>
    public abstract class EFRepository<TEntity, TEFContext> : IRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
        where TEFContext : EFContext
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="context"></param>
        public EFRepository(TEFContext context)
        {
            this.EFContext = context;
        }

        /// <summary>
        /// 上下文
        /// </summary>
        protected virtual TEFContext EFContext { get; set; }

        /// <summary>
        /// 工作单元
        /// </summary>
        public virtual IUnitOfWork UnitOfWork => EFContext;


        #region 查
        public virtual TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> efContext = EFContext.RayDbSet<TEntity>().AsNoTracking(); //不跟踪状态
            IQueryable<TEntity> res = efContext.Where(filter);
            return res.FirstOrDefault();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetAllMatching(x => true);
        }

        public virtual IQueryable<TEntity> GetAllMatching(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> efContext = EFContext.RayDbSet<TEntity>().AsNoTracking();
            var res = efContext.Where(filter);
            return res;
        }

        public virtual PageResult<TEntity> GetListPaged<TK>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc)
        {
            var efContext = GetAll();
            return GetListPaged(efContext, pageIndex, pageSize, filter, orderByExpression, sortOrder);
        }

        public virtual PageResult<TEntity> GetListPaged<TK>(IQueryable<TEntity> queryAggres,
            int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc)
        {
            var source = queryAggres.Where(filter);

            int preCount = pageSize * (pageIndex - 1);
            int totalCount = source.Count();

            //排序
            IQueryable<TEntity> sourceOrder = source;
            switch (sortOrder)
            {
                case SortEnum.Asc:
                    if (orderByExpression != null)
                        sourceOrder = source.OrderBy(orderByExpression);
                    break;
                case SortEnum.Desc:
                    if (orderByExpression != null)
                        sourceOrder = source.OrderByDescending(orderByExpression);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }
            //分页
            IQueryable<TEntity> sourcePage = sourceOrder.Skip(preCount).Take(pageSize);
            int totalPages = totalCount > 0
                ? (int)Math.Ceiling((double)totalCount / (double)pageSize)
                : 0;
            return new PageResult<TEntity>()
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

        public virtual bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return GetAllMatching(filter)
                .Any();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> efContext = GetAll();
            return efContext.Count(filter);
        }
        #endregion

        #region 增
        public virtual TEntity Add(TEntity entity)
        {
            EntityEntry<TEntity> entry = EFContext.Add(entity);
            UnitOfWork.SaveChanges();
            return entry.Entity;
        }

        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> tAggregateRoots)
        {
            EntityEntry<IEnumerable<TEntity>> entities = EFContext.Add(tAggregateRoots);
            UnitOfWork.SaveChanges();
            return entities.Entity;
        }

        public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(this.Add(entity));
        }
        #endregion

        #region 改
        public virtual void Update(TEntity entity, params Expression<Func<TEntity, dynamic>>[] ignoreFields)
        {
            var ignoreFiledList = new List<string>();
            foreach (var value in ignoreFields)
            {
                var member = value.GetMember();
                var propertyName = member?.Name;
                if (!string.IsNullOrEmpty(propertyName) && !ignoreFiledList.Exists(x => x == propertyName))
                    ignoreFiledList.Add(propertyName);
            }

            if (entity != null)
            {
                EFContext.Entry(entity).State = EntityState.Modified;
            }

            ignoreFiledList = ignoreFiledList.Where(m => !string.IsNullOrWhiteSpace(m))
                .Select(m => m.Trim())
                .ToList(); //属性去除空格容错
            var itemPropList = entity.GetType().GetProperties();
            foreach (var itemProp in itemPropList)
            {
                var itemPropName = itemProp.Name;
                if (ignoreFiledList.Contains(itemPropName, StringComparer.OrdinalIgnoreCase))
                {
                    EFContext.Entry(entity).Property(itemPropName).IsModified = false;
                }
            }
            UnitOfWork.SaveChanges();
        }

        public void Update(IQueryable<TEntity> tAggregateRoots, params Expression<Func<TEntity, dynamic>>[] ignoreFields)
        {
            tAggregateRoots.Each(x => Update(x, ignoreFields));
        }

        public void UpdateWithField(TEntity item, params Expression<Func<TEntity, dynamic>>[] updateFields)
        {
            if (updateFields == null || !updateFields.Any())
                Update(item);

            var updateFiledList = new List<string>();
            foreach (var value in updateFields)
            {
                var member = value.GetMember();
                var propertyName = member?.Name;
                if (!string.IsNullOrEmpty(propertyName) && !updateFiledList.Exists(x => x == propertyName))
                    updateFiledList.Add(propertyName);
            }

            if (item != null)
            {
                EFContext.Entry(item).State = EntityState.Modified;
            }

            updateFiledList = updateFiledList.Where(m => !string.IsNullOrWhiteSpace(m))
                .Select(m => m.Trim())
                .ToList(); //属性去除空格容错
            var itemPropList = item.GetType().GetProperties();
            foreach (var itemProp in itemPropList)
            {
                var itemPropName = itemProp.Name;
                EFContext.Entry(item).Property(itemPropName).IsModified =
                    updateFiledList.Contains(itemPropName, StringComparer.OrdinalIgnoreCase);
            }
        }

        public void UpdateWithField(IQueryable<TEntity> tAggregateRoots, params Expression<Func<TEntity, dynamic>>[] updateFields)
        {
            tAggregateRoots.Each(x => UpdateWithField(x, updateFields));
        }

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> value)
        {
            throw new NotImplementedException();
            /*
            IQueryable<TEntity> entities = EFContext.RayDbSet<TEntity>()
                     .AsNoTracking()
                     .Where(filter)
                     .Set(value);
            entities.ForEachAsync(x => Update(x));
            */
        }

        public void UpSert(TEntity entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 删
        public void Remove(Entity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(IQueryable<TEntity> tAggregateRoots)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Entity entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }


    public abstract class EFRepository<TEntity, TKey, TEFContext> : EFRepository<TEntity, TEFContext>, IRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot where TEFContext : EFContext
    {
        public EFRepository(TEFContext context) : base(context)
        {
        }

        #region 查
        public virtual TEntity Find(TKey id)
        {
            return this.Find(x => x.Id.ToString() == id.ToString());
        }

        public virtual Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(this.Find(id));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public override PageResult<TEntity> GetListPaged<TK>(IQueryable<TEntity> queryAggres,
            int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc)
        {
            var source = queryAggres.Where(filter);

            int preCount = pageSize * (pageIndex - 1);
            int totalCount = source.Count();

            //排序
            IQueryable<TEntity> sourceOrder = source;
            switch (sortOrder)
            {
                case SortEnum.Asc:
                    sourceOrder = orderByExpression != null
                        ? source.OrderBy(orderByExpression)
                        : source.OrderBy(x => x.Id);
                    break;
                case SortEnum.Desc:
                    sourceOrder = orderByExpression != null
                        ? source.OrderByDescending(orderByExpression)
                        : source.OrderByDescending(x => x.Id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }
            //分页
            IQueryable<TEntity> sourcePage = sourceOrder.Skip(preCount).Take(pageSize);
            int totalPages = totalCount > 0
                ? (int)Math.Ceiling((double)totalCount / (double)pageSize)
                : 0;
            return new PageResult<TEntity>()
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
        #endregion

        #region 改

        #endregion

        #region 删
        public void Remove(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TKey id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public abstract class IntegratedRepository<TEntity, TEFContext> : EFRepository<TEntity, long, TEFContext>, IIntegratedRepository<TEntity>
        where TEntity : IntegratedEntity, IAggregateRoot
        where TEFContext : EFContext
    {
        public IntegratedRepository(TEFContext context) : base(context)
        {

        }

        #region 查
        public virtual TEntity Find(long id, bool isIgnoreDelete)
        {
            IQueryable<TEntity> efContext = EFContext.RayDbSet<TEntity>().AsNoTracking();
            IQueryable<TEntity> res = efContext.Where(x => x.Id == id);
            if (isIgnoreDelete)
                res = res.Where(x => x.IsDeleted == !isIgnoreDelete);
            return res.FirstOrDefault();
        }

        public virtual IQueryable<TEntity> GetAll(bool isIgnoreDelete)
        {
            return GetAllMatching(x => true, isIgnoreDelete);
        }

        public virtual IQueryable<TEntity> GetAllMatching(Expression<Func<TEntity, bool>> filter, bool isIgnoreDelete)
        {
            IQueryable<TEntity> efContext = EFContext.RayDbSet<TEntity>().AsNoTracking();
            IQueryable<TEntity> res = efContext.Where(filter);
            if (isIgnoreDelete)
                res = res.Where(x => x.IsDeleted == !isIgnoreDelete);
            return res;
        }

        public virtual PageResult<TEntity> GetListPaged<TK>(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> filter,
            bool isIgnoreDelete,
            Expression<Func<TEntity, TK>> orderByExpression = null, SortEnum sortOrder = SortEnum.Asc)
        {
            IQueryable<TEntity> efContext = GetAll(isIgnoreDelete);
            return GetListPaged(efContext, pageIndex, pageSize, filter, orderByExpression, sortOrder);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter, bool isIgnoreDelete)
        {
            return GetAllMatching(filter, isIgnoreDelete)
                .Any();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter, bool isIgnoreDelete)
        {
            IQueryable<TEntity> efContext = GetAll(isIgnoreDelete);
            return efContext.Count(filter);
        }

        #endregion

        #region 删
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Delete(IQueryable<TEntity> tAggregateRoots)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 改

        #endregion
    }

}
