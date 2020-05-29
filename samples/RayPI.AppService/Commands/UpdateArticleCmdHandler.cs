using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Commands
{
    public class UpdateArticleCmdHandler : RequestHandler<UpdateArticleCmd, Guid>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        public UpdateArticleCmdHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        protected override Guid Handle(UpdateArticleCmd request)
        {
            var entity = _articleRepository.GetAsync(x => x.Id == request.Id).Result;

            entity.Title = request.Title;
            entity.SubTitle = request.SubTitle;
            entity.Content = request.Content;

            var result = _articleRepository.UpdateAsync(entity).Result;

            return result.Id;
        }
    }
}
