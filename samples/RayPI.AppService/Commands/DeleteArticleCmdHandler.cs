using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Commands
{
    public class DeleteArticleCmdHandler : RequestHandler<DeleteArticleCmd>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        public DeleteArticleCmdHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        protected override async void Handle(DeleteArticleCmd request)
        {
            var entity = _articleRepository.GetAsync(x => x.Id == request.Id).Result;
            await _articleRepository.DeleteAsync(entity);
        }
    }
}
