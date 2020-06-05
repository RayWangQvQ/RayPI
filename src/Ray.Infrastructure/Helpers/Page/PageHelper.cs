using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ray.Infrastructure.Helpers.Page
{
    public class PageHelper
    {
        public static PageResult<T> Page<T>(IEnumerable<T> source,
            int pageSize,
            int pageIndex,
            Expression<Func<T, bool>> filter = null)
        {
            return Page<T>(source.AsQueryable(), pageSize, pageIndex, filter);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static PageResult<T> Page<T>(IQueryable<T> source,
            int pageSize,
            int pageIndex,
            Expression<Func<T, bool>> filter = null)
        {
            return PageOrder<T, string>(source, pageSize, pageIndex, orderByExpression: null, filter: filter);
        }

        /// <summary>
        /// 排序分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static PageResult<T> PageOrder<T, TK>(IEnumerable<T> source,
            int pageSize,
            int pageIndex,
            Expression<Func<T, TK>> orderByExpression,
            SortEnum sortOrder = SortEnum.Original,
            Expression<Func<T, bool>> filter = null)
        {
            return PageOrder<T, TK>(source.AsQueryable(), pageSize, pageIndex, orderByExpression: orderByExpression, sortOrder: sortOrder, filter: filter);
        }

        /// <summary>
        /// 排序分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static PageResult<T> PageOrder<T, TK>(IQueryable<T> source,
            int pageSize,
            int pageIndex,
            Expression<Func<T, TK>> orderByExpression,
            SortEnum sortOrder = SortEnum.Original,
            Expression<Func<T, bool>> filter = null)
        {
            int skipCount;//跳过条数
            int totalCount;//筛选后总条数
            int totalPages;//筛选后总页数

            //筛选
            if (filter != null) source = source.Where(filter);
            totalCount = source.Count();
            totalPages = totalCount > 0
                ? (int)Math.Ceiling((double)totalCount / (double)pageSize)
                : 0;
            pageIndex = totalPages <= 0
                ? 1
                : pageIndex > totalPages
                    ? totalPages
                    : pageIndex;
            skipCount = pageSize * (pageIndex - 1)

            //排序
            if (orderByExpression != null) source = Order(source, orderByExpression, sortOrder);

            //分页
            source = source.Skip(skipCount).Take(pageSize);

            return new PageResult<T>
            {
                List = source.ToList(),
                PageIndex = totalPages <= 0 ? 1 : (pageIndex > totalPages ? totalPages : pageIndex),
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }


        public static Dictionary<int, List<T>> PageOrderToDic<T, TK>(IQueryable<T> source,
            int pageSize,
            Expression<Func<T, TK>> orderByExpression,
            SortEnum sortOrder = SortEnum.Original,
            Expression<Func<T, bool>> filter = null)
        {
            var dic = new Dictionary<int, List<T>>();

            //筛选
            if (filter != null) source = source.Where(filter);

            //排序
            if (orderByExpression != null) source = Order(source, orderByExpression, sortOrder);

            int pageCount = source.Count() / pageSize + 1;

            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                int skipCount = pageSize * (pageIndex - 1);//跳过条数
                dic.Add(pageIndex, source.Skip(skipCount).Take(pageSize).ToList());
            }

            return dic;
        }

        private static IQueryable<T> Order<T, TK>(IQueryable<T> source,
            Expression<Func<T, TK>> orderByExpression,
            SortEnum sortOrder = SortEnum.Original)
        {
            //排序
            switch (sortOrder)
            {
                case SortEnum.Original:
                    break;
                case SortEnum.Asc:
                    source = source.OrderBy(orderByExpression);
                    break;
                case SortEnum.Desc:
                    source = source.OrderByDescending(orderByExpression);
                    break;
            }
            return source;
        }
    }
}
