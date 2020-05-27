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
    public class CreateArticleCmdHandler : RequestHandler<CreateArticleCmd, long>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        public CreateArticleCmdHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        protected override long Handle(CreateArticleCmd request)
        {
            var entity = new Article
            {
                Title = request.Title,
                SubTitle = request.SubTitle,
                Content = request.Content
            };

            long id = _articleRepository.Add(entity).Id;
            return id;
        }
    }
}
