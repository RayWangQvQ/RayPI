using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.ArticleApp.Queries
{
    public class ArticleQueryHandler : IRequestHandler<QueryArticleDto, ResponseQueryArticleDto>
    {
        private readonly IMyBaseRepository<Domain.Entity.Article> _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticleQueryHandler(IMyBaseRepository<Domain.Entity.Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public async Task<ResponseQueryArticleDto> Handle(QueryArticleDto request, CancellationToken cancellationToken)
        {
            Domain.Entity.Article entity = await _articleRepository.FindAsync(x => x.Id == request.Id);
            return AutoMapperHelper.Map<Domain.Entity.Article, ResponseQueryArticleDto>(entity);
        }
    }
}
