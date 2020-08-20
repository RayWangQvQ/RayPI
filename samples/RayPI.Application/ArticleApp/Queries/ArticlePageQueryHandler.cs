using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Queries
{
    public class ArticlePageQueryHandler : IRequestHandler<QueryArticlePageDto, List<ResponseQueryArticleDto>>
    {
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticlePageQueryHandler(IArticleRepository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public Task<List<ResponseQueryArticleDto>> Handle(QueryArticlePageDto request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entity.Article> queryable = _articleRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
                queryable = queryable.Where(x => x.Title.Contains(request.Title));

            //return AutoMapperHelper.Map<List<Article>, List<ArticleQueryViewModel>>(list);
            var list = queryable.ToList()
                .Select(x => AutoMapperHelper.Map<Domain.Entity.Article, ResponseQueryArticleDto>(x))
                .ToList();
            return Task.FromResult(list);
        }
    }
}
