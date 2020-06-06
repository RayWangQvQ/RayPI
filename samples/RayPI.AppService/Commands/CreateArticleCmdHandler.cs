using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Commands
{
    public class CreateArticleCmdHandler : IRequestHandler<CreateArticleCmd, Guid>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        public CreateArticleCmdHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public async Task<Guid> Handle(CreateArticleCmd request, CancellationToken cancellationToken)
        {
            var entity = new Article(request.Title)
            {
                SubTitle = request.SubTitle,
                Content = request.Content
            };

            Article result = await _articleRepository.InsertAsync(entity, true);
            return result.Id;
        }
    }
}
