using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ray.Application.IAppServices;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Auditing;
using Ray.Infrastructure.Extensions;
using Ray.Infrastructure.Extensions.Linq;
using Ray.Infrastructure.Page;

namespace Ray.Application.AppServices
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TGetOutputDto"></typeparam>
    /// <typeparam name="TGetListOutputDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class QueryAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
        : AppService,
         IQueryAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// 仓储
        /// </summary>
        protected IRepositoryBase<TEntity, TKey> Repository { get; }

        protected QueryAppService(IRepositoryBase<TEntity, TKey> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// 鉴权策略名称-获取详情
        /// </summary>
        protected virtual string GetPolicyName { get; set; }

        /// <summary>
        /// 鉴权策略名称-获取列表分页
        /// </summary>
        protected virtual string GetPagePolicyName { get; set; }


        public async Task<TGetOutputDto> GetAsync(TKey id)
        {
            await CheckGetPolicyAsync();

            var entity = await GetEntityByIdAsync(id);
            return MapToGetOutputDto(entity);
        }

        public async Task<PageResultDto<TGetListOutputDto>> GetPageAsync(TGetListInput input)
        {
            await CheckGetListPolicyAsync();

            IQueryable<TEntity> query = CreateFilteredQuery(input);

            var totalCount = query.Count();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            var page = input as IPageResultRequest;
            return new PageResultDto<TGetListOutputDto>()
            {
                TotalCount = totalCount,
                PageIndex = page?.PageIndex ?? 0,
                PageSize = page?.PageSize ?? 0,
                List = entities.Select(MapToGetPageOutputDto).ToList()
            };
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<TEntity> GetEntityByIdAsync(TKey id)
        {
            return await Repository.GetAsync(id);
        }

        #region 鉴权
        protected virtual async Task CheckGetPolicyAsync()
        {
            await CheckPolicyAsync(GetPolicyName);
        }

        protected virtual async Task CheckGetListPolicyAsync()
        {
            await CheckPolicyAsync(GetPagePolicyName);
        }
        #endregion

        #region Map映射
        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetOutputDto"/>.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overriden for custom mapping.
        /// </summary>
        protected virtual TGetOutputDto MapToGetOutputDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntity, TGetOutputDto>(entity);
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetListOutputDto"/>.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overriden for custom mapping.
        /// </summary>
        protected virtual TGetListOutputDto MapToGetPageOutputDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntity, TGetListOutputDto>(entity);
        }
        #endregion

        /// <summary>
        /// This method should create <see cref="IQueryable{TEntity}"/> based on given input.
        /// It should filter query if needed, but should not do sorting or paging.
        /// Sorting should be done in <see cref="ApplySorting"/> and paging should be done in <see cref="ApplyPaging"/>
        /// methods.
        /// </summary>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> CreateFilteredQuery(TGetListInput input)
        {
            return Repository.GetQueryable();
        }

        #region 排序
        /// <summary>
        /// Should apply sorting if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetListInput input)
        {
            //Try to sort query if available
            if (input is ISortedResultRequest sortInput)
            {
                if (!sortInput.Sorting.IsNullOrWhiteSpace())
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            //IQueryable.Task requires sorting, so we should sort if Take will be used.
            if (input is IPageResultRequest)
            {
                return ApplyDefaultSorting(query);
            }

            //No sorting
            return query;
        }

        /// <summary>
        /// Applies sorting if no sorting specified but a limited result requested.
        /// </summary>
        /// <param name="query">The query.</param>
        protected virtual IQueryable<TEntity> ApplyDefaultSorting(IQueryable<TEntity> query)
        {
            if (typeof(TEntity).IsAssignableTo<IHasCreationAuditing>())
            {
                return query.OrderByDescending(e => ((IHasCreationAuditing)e).CreationTime);
            }

            throw new Exception("No sorting specified but this query requires sorting. Override the ApplyDefaultSorting method for your application service derived from QueryAppService!");
        }
        #endregion

        #region 分页
        /// <summary>
        /// Should apply paging if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListInput input)
        {
            //Try to use paging if available
            if (input is IPageResultRequest pagedInput)
            {
                return query.PageBy(pagedInput.PageSize, pagedInput.PageIndex);
            }

            //No paging
            return query;
        }
        #endregion
    }
}
