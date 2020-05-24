using System;
using System.Collections.Generic;
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
    public class ArticleQueryHandler : RequestHandler<ArticleQuery, ArticleQueryViewModel>
    {
        private readonly IBaseRepository<ArticleEntity> _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticleQueryHandler(IBaseRepository<ArticleEntity> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        protected override ArticleQueryViewModel Handle(ArticleQuery request)
        {
            var entity = _articleRepository.Find(request.Id);
            return AutoMapperHelper.Map<ArticleEntity, ArticleQueryViewModel>(entity);
        }
    }
}
