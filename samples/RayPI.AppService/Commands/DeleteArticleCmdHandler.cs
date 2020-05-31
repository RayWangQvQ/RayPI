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
    public class DeleteArticleCmdHandler : IRequestHandler<DeleteArticleCmd>
    {
        private readonly IBaseRepository<Article> _articleRepository;

        public DeleteArticleCmdHandler(IBaseRepository<Article> baseRepository)
        {
            this._articleRepository = baseRepository;
        }

        public async Task<Unit> Handle(DeleteArticleCmd request, CancellationToken cancellationToken)
        {
            Article entity = _articleRepository.GetAsync(x => x.Id == request.Id).Result;
            await _articleRepository.DeleteAsync(entity, true);
            //todo:
            var i = 0;
            return Unit.Value;
        }
    }
}
