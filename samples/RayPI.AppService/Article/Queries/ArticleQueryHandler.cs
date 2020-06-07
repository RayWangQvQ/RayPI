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
    public class ArticleQueryHandler : IRequestHandler<ArticleQuery, ArticleQueryViewModel>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticleQueryHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public async Task<ArticleQueryViewModel> Handle(ArticleQuery request, CancellationToken cancellationToken)
        {
            Article entity = await _articleRepository.FindAsync(x => x.Id == request.Id);
            return AutoMapperHelper.Map<Article, ArticleQueryViewModel>(entity);
        }
    }
}
