using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Application.AppServices;
using Ray.Infrastructure.Page;
using RayPI.Application.ArticleApp.Dtos;
using RayPI.Domain.Aggregates.ArticleAggregate;

namespace RayPI.Application.ArticleApp.Queries
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
