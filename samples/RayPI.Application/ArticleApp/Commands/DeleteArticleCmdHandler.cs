using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.ArticleApp.Commands
{
    public class DeleteArticleCmdHandler : IRequestHandler<DeleteArticleDto>
    {
        private readonly IBaseRepository<Domain.Entity.Article> _articleRepository;

        public DeleteArticleCmdHandler(IBaseRepository<Domain.Entity.Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public async Task<Unit> Handle(DeleteArticleDto request, CancellationToken cancellationToken)
        {
            Domain.Entity.Article entity = _articleRepository.GetAsync(x => x.Id == request.Id).Result;
            await _articleRepository.DeleteAsync(entity, true);
            return default;
        }
    }
}
