using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Application.AppServices;
using RayPI.Application.ArticleApp.Dtos;
using RayPI.Domain.Aggregates.ArticleAggregate;

namespace RayPI.Application.ArticleApp.Commands
{
    public class CreateArticleCmdHandler
        : CrudAppService<Article, Guid, CreateArticleDto, ArticleDetailDto>,
            IRequestHandler<CreateArticleDto, ArticleDetailDto>
    {

        public CreateArticleCmdHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ArticleDetailDto> Handle(CreateArticleDto request, CancellationToken cancellationToken)
        {
            return await CreateAsync(request);
        }
    }
}
