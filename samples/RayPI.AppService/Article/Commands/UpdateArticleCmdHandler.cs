using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RayPI.AppService.Article.Dtos;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Article.Commands
{
    public class UpdateArticleCmdHandler : IRequestHandler<UpdateArticleCmd, Guid>
    {
        private readonly IBaseRepository<Domain.Entity.Article> _articleRepository;

        public UpdateArticleCmdHandler(IBaseRepository<Domain.Entity.Article> baseRepository)
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
