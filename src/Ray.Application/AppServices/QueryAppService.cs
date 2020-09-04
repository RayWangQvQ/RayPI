using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ray.Application.IAppServices;
using Ray.Auditing;
using Ray.Domain.Entities;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Extensions;
using Ray.Infrastructure.Extensions.Linq;
using Ray.Infrastructure.Page;

namespace Ray.Application.AppServices
{
    /// <summary>
    /// 查询AppService
    /// </summary>
    /// <typeparam name="TGetDetailOutputDto"></typeparam>
    /// <typeparam name="TGetListItemOutputDto"></typeparam>
    /// <typeparam name="TEntityKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class QueryAppService<TEntity, TEntityKey, TGetListInput, TGetDetailOutputDto, TGetListItemOutputDto>
        : AppService,
         IQueryAppService<TEntityKey, TGetListInput, TGetDetailOutputDto, TGetListItemOutputDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        /// <summary>
        /// 仓储
        /// </summary>
        protected virtual IBaseRepository<TEntity, TEntityKey> Repository => this.ServiceProvider.GetRequiredService<IBaseRepository<TEntity, TEntityKey>>();

        public QueryAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 鉴权策略名称-获取详情
        /// </summary>
        protected virtual string GetPolicyName { get; set; }

        /// <summary>
        /// 鉴权策略名称-获取列表分页
        /// </summary>
        protected virtual string GetPagePolicyName { get; set; }


        public async Task<TGetDetailOutputDto> GetAsync(TEntityKey id)
        {
            await CheckGetPolicyAsync();

            var entity = await GetEntityByIdAsync(id);
            return MapToGetOutputDto(entity);
        }

        public async Task<PageResultDto<TGetListItemOutputDto>> GetPageAsync(TGetListInput input)
        {
            await CheckGetListPolicyAsync();

            IQueryable<TEntity> query = CreateFilteredQuery(input);

            var totalCount = query.Count();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            //var entities = await query.ToListAsync();//todo：暂时未实现异步
            var entities = await Task.FromResult(query.ToList());

            var page = input as IPageRequest;
            return new PageResultDto<TGetListItemOutputDto>()
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
        protected async Task<TEntity> GetEntityByIdAsync(TEntityKey id)
        {
            return await Repository.GetAsync(id);
        }

        /// <summary>
        /// 通过入参获取筛选后的 <see cref="IQueryable"/>，有继承的AppService根据具体的入参和业务覆写实现
        /// 它只负责筛选，不负责排序和分页.
        /// 排序可通过 <see cref="ApplySorting"/> 实现，分页可通过 <see cref="ApplyPaging"/> 实现
        /// methods.
        /// </summary>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> CreateFilteredQuery(TGetListInput input)
        {
            return Repository.GetQueryable();
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
        /// 将 <see cref="TEntity"/> 映射为 <see cref="TGetDetailOutputDto"/>.
        /// 默认使用 <see cref="IRayMapper"/> 进行映射.
        /// </summary>
        protected virtual TGetDetailOutputDto MapToGetOutputDto(TEntity entity)
        {
            return RayMapper.Map<TEntity, TGetDetailOutputDto>(entity);
        }

        /// <summary>
        /// 将 <see cref="TEntity"/> 映射为 <see cref="TGetListItemOutputDto"/>.
        /// 默认使用 <see cref="IRayMapper"/> 进行映射.
        /// </summary>
        protected virtual TGetListItemOutputDto MapToGetPageOutputDto(TEntity entity)
        {
            return RayMapper.Map<TEntity, TGetListItemOutputDto>(entity);
        }
        #endregion

        #region 排序
        /// <summary>
        /// 尝试附加排序操作
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetListInput input)
        {
            //如果实现了ISortRequest，则直接按照Sorting字符串排序
            if (input is ISortRequest sortInput)
            {
                if (!sortInput.Sorting.IsNullOrWhiteSpace())
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            //IQueryable.Task 需要排序, 如果分页需要使用Take，那么需要附加一个默认排序
            if (input is IPageRequest)
            {
                return ApplyDefaultSorting(query);
            }

            //No sorting
            return query;
        }

        /// <summary>
        /// 附加默认排序（如果请求分页但又没有指定排序，则需要附加默认排序）
        /// </summary>
        /// <param name="query">The query.</param>
        protected virtual IQueryable<TEntity> ApplyDefaultSorting(IQueryable<TEntity> query)
        {
            //如果有创建时间，则按照创建时间倒序排序
            if (typeof(TEntity).IsAssignableTo<IHasCreationAuditing>())
            {
                return query.OrderByDescending(e => ((IHasCreationAuditing)e).CreationTime);
            }

            //如果有Id，则按照Id倒叙排序
            if (typeof(TEntity).GetProperty("Id") != null)
            {
                return query.OrderBy("Id Desc");
            }

            throw new Exception("No sorting specified but this query requires sorting. Override the ApplyDefaultSorting method for your application service derived from QueryAppService!");
        }
        #endregion

        #region 分页
        /// <summary>
        /// 尝试附加分页操作
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListInput input)
        {
            //如果实现了IPageRequest，则按照要求分页
            if (input is IPageRequest pagedInput)
            {
                return query.PageBy(pagedInput.PageSize, pagedInput.PageIndex);
            }

            //没有实现则直接返回
            return query;
        }
        #endregion
    }


    public class QueryAppService<TEntity, TEntityKey, TGetListInput, TOutputDto>
        : QueryAppService<TEntity, TEntityKey, TGetListInput, TOutputDto, TOutputDto>,
            IQueryAppService<TEntityKey, TGetListInput, TOutputDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        public QueryAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }

    public class QueryAppService<TEntity, TEntityKey, TOutputDto>
        : QueryAppService<TEntity, TEntityKey, PageAndSortRequest, TOutputDto, TOutputDto>,
            IQueryAppService<TEntityKey, TOutputDto>
        where TEntity : class, IEntity<TEntityKey>
    {
        public QueryAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
