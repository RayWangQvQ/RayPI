using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RayPI.AppService.Article.Dtos;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Article.Commands
{
    public class CreateArticleCmdHandler : IRequestHandler<CreateArticleCmd, Guid>
    {
        private readonly IBaseRepository<Domain.Entity.Article> _articleRepository;

        public CreateArticleCmdHandler(IBaseRepository<Domain.Entity.Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public async Task<Guid> Handle(CreateArticleCmd request, CancellationToken cancellationToken)
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
