using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ray.Application.AppServices;
using Ray.Infrastructure.Helpers;
using Ray.Infrastructure.Page;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Queries
{
    public class ArticlePageQueryHandler
        : QueryAppService<Article, Guid, QueryArticlePageDto, ArticleDetailDto>,
            IRequestHandler<QueryArticlePageDto, PageResultDto<ArticleDetailDto>>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ArticlePageQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<PageResultDto<ArticleDetailDto>> Handle(QueryArticlePageDto request, CancellationToken cancellationToken)
        {
            return await GetPageAsync(request);
        }

        /// <summary>
        /// 覆写筛选方法，实现根据Title筛选
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Article> CreateFilteredQuery(QueryArticlePageDto input)
        {
            IQueryable<Article> query = Repository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(input.Title))
            {
                query = query.Where(x => x.Title.Contains(input.Title));
            }

            return query;
        }
    }
}
