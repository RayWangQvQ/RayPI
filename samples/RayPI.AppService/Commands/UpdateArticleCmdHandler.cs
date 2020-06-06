using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Helpers;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Commands
{
    public class UpdateArticleCmdHandler : IRequestHandler<UpdateArticleCmd, Guid>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        public UpdateArticleCmdHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public async Task<Guid> Handle(UpdateArticleCmd request, CancellationToken cancellationToken)
        {
            var entity = await _articleRepository.GetAsync(x => x.Id == request.Id);

            entity.Title = request.Title;
            entity.SubTitle = request.SubTitle;
            entity.Content = request.Content;

            var result = await _articleRepository.UpdateAsync(entity);

            return result.Id;
        }
    }
}
