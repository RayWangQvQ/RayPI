using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Application.AppServices;
using RayPI.AppService.ArticleApp.Dtos;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepositories;

namespace RayPI.AppService.ArticleApp.Commands
{
    public class DeleteArticleCmdHandler
        : CrudAppService<Article, Guid, DeleteArticleDto>,
            IRequestHandler<DeleteArticleDto, bool>
    {
        public DeleteArticleCmdHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<bool> Handle(DeleteArticleDto request, CancellationToken cancellationToken)
        {
            await this.DeleteAsync(request.Id);
            return true;
        }
    }
}
