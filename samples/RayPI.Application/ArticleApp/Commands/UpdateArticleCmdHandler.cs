using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Application.AppServices;
using RayPI.Application.ArticleApp.Dtos;
using RayPI.Domain.Aggregates.ArticleAggregate;

namespace RayPI.Application.ArticleApp.Commands
{
    public class UpdateArticleCmdHandler
        : CrudAppService<Article, Guid, UpdateArticleDto, ArticleDetailDto>,
            IRequestHandler<UpdateArticleDto, ArticleDetailDto>
    {
        public UpdateArticleCmdHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<ArticleDetailDto> Handle(UpdateArticleDto request, CancellationToken cancellationToken)
        {
            return await this.UpdateAsync(request.Id, request);
        }
    }
}
