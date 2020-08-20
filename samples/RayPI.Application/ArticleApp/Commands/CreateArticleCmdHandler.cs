using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Commands
{
    public class CreateArticleCmdHandler : IRequestHandler<CreateArticleDto, Guid>
    {
        private readonly IArticleRepository _articleRepository;

        public CreateArticleCmdHandler(IArticleRepository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public async Task<Guid> Handle(CreateArticleDto request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entity.Article(request.Title)
            {
                SubTitle = request.SubTitle,
                Content = request.Content
            };

            Domain.Entity.Article result = await _articleRepository.InsertAsync(entity, true);
            return result.Id;
        }
    }
}
