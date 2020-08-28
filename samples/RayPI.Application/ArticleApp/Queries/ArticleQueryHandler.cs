using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Helpers;
using Ray.Infrastructure.ObjectMap.AutoMapper;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.Entity;
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
            Article entity = await _articleRepository.FindAsync(x => x.Id == request.Id);
            return AutoMapperHelper.Map<Domain.Entity.Article, ResponseQueryArticleDto>(entity);
        }
    }
}
