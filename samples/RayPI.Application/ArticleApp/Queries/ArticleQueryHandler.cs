using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Queries
{
    public class ArticleQueryHandler : IRequestHandler<QueryArticleDto, ResponseQueryArticleDto>
    {
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        public ArticleQueryHandler(IArticleRepository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public async Task<ResponseQueryArticleDto> Handle(QueryArticleDto request, CancellationToken cancellationToken)
        {
            Domain.Entity.Article entity = await _articleRepository.FindAsync(x => x.Id == request.Id);
            return AutoMapperHelper.Map<Domain.Entity.Article, ResponseQueryArticleDto>(entity);
        }
    }
}
