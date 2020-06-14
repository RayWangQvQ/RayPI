using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.AppService.Article.Dtos;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Article.Queries
{
    public class ArticlePageQueryHandler : IRequestHandler<QueryArticlePageDto, List<ResponseQueryArticleDto>>
    {
        private readonly IBaseRepository<Domain.Entity.Article> _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticlePageQueryHandler(IBaseRepository<Domain.Entity.Article> baseRepository)
        {
            this._articleRepository = baseRepository;
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
