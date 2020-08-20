using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Commands
{
    public class UpdateArticleCmdHandler : IRequestHandler<UpdateArticleDto, Guid>
    {
        private readonly IArticleRepository _articleRepository;

        public UpdateArticleCmdHandler(IArticleRepository articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        public async Task<Guid> Handle(UpdateArticleDto request, CancellationToken cancellationToken)
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
