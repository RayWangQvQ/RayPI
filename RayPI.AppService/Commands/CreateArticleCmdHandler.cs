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
    public class CreateArticleCmdHandler : IRequestHandler<CreateArticleCmd, long>
    {
        private readonly IBaseRepository<ArticleEntity> _articleRepository;

        public CreateArticleCmdHandler(IBaseRepository<ArticleEntity> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public Task<long> Handle(CreateArticleCmd request, CancellationToken cancellationToken)
        {
            var entity = new ArticleEntity
            {
                Title = request.Title,
                SubTitle = request.SubTitle,
                Content = request.Content
            };

            return Task.Run(() =>
            {
                long id = _articleRepository.Add(entity);
                return id;
            });
        }
    }
}
