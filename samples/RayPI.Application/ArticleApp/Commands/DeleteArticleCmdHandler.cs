using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Commands
{
    public class DeleteArticleCmdHandler : IRequestHandler<DeleteArticleDto>
    {
        private readonly IArticleRepository _articleRepository;

        public DeleteArticleCmdHandler(IArticleRepository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public async Task<Unit> Handle(DeleteArticleDto request, CancellationToken cancellationToken)
        {
            Article entity = _articleRepository.GetAsync(x => x.Id == request.Id).Result;
            await _articleRepository.DeleteAsync(entity, true);
            return default;
        }
    }
}
