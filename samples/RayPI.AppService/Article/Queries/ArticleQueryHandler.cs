using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.AppService.Article.Dtos;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Article.Queries
{
    public class ArticleQueryHandler : IRequestHandler<ArticleQuery, ArticleQueryViewModel>
    {
        private readonly IBaseRepository<Domain.Entity.Article> _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticleQueryHandler(IBaseRepository<Domain.Entity.Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public async Task<ArticleQueryViewModel> Handle(ArticleQuery request, CancellationToken cancellationToken)
        {
            Domain.Entity.Article entity = await _articleRepository.FindAsync(x => x.Id == request.Id);
            return AutoMapperHelper.Map<Domain.Entity.Article, ArticleQueryViewModel>(entity);
        }
    }
}
