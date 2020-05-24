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
        private readonly IBaseRepository<ArticleEntity> _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticlePageQueryHandler(IBaseRepository<ArticleEntity> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        protected override List<ArticleQueryViewModel> Handle(ArticlePageQuery request)
        {
            IQueryable<ArticleEntity> entityQuerable = _articleRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(request.Title))
                entityQuerable = entityQuerable.Where(x => x.Title.Contains(request.Title));

            //var list = AutoMapperHelper.Map<List<ArticleEntity>, List<ArticleQueryViewModel>>(entityQuerable.ToList());
            var list = entityQuerable.ToList()
                .Select(x => AutoMapperHelper.Map<ArticleEntity, ArticleQueryViewModel>(x))
                .ToList();
            return list;
        }
    }
}
