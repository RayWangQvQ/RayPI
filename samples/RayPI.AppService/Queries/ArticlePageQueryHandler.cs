using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.AppService.Queries.ViewModels;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Queries
{
    public class ArticlePageQueryHandler : RequestHandler<ArticlePageQuery, List<ArticleQueryViewModel>>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticlePageQueryHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        protected override List<ArticleQueryViewModel> Handle(ArticlePageQuery request)
        {
            IQueryable<Article> queryable = _articleRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
                queryable = queryable.Where(x => x.Title.Contains(request.Title));

            //return AutoMapperHelper.Map<List<Article>, List<ArticleQueryViewModel>>(list);
            return queryable.ToList()
                .Select(x => AutoMapperHelper.Map<Article, ArticleQueryViewModel>(x))
                .ToList();
        }
    }
}
