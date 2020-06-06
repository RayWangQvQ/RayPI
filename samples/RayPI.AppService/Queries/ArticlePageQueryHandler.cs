using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.AppService.Queries.ViewModels;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Queries
{
    public class ArticlePageQueryHandler : IRequestHandler<ArticlePageQuery, List<ArticleQueryViewModel>>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticlePageQueryHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public Task<List<ArticleQueryViewModel>> Handle(ArticlePageQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Article> queryable = _articleRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
                queryable = queryable.Where(x => x.Title.Contains(request.Title));

            //return AutoMapperHelper.Map<List<Article>, List<ArticleQueryViewModel>>(list);
            var list = queryable.ToList()
                .Select(x => AutoMapperHelper.Map<Article, ArticleQueryViewModel>(x))
                .ToList();
            return Task.FromResult(list);
        }
    }
}
