using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Commands
{
    public class UpdateArticleCmdHandler : RequestHandler<UpdateArticleCmd, long>
    {
        private readonly IBaseRepository<ArticleEntity> _articleRepository;

        public UpdateArticleCmdHandler(IBaseRepository<ArticleEntity> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        protected override long Handle(UpdateArticleCmd request)
        {
            var entity = _articleRepository.FindById(request.Id);
            if (entity == null) throw new Exception("不存在");

            entity.Title = request.Title;
            entity.SubTitle = request.SubTitle;
            entity.Content = request.Content;
            _articleRepository.Update(entity);

            return request.Id;
        }
    }
}
