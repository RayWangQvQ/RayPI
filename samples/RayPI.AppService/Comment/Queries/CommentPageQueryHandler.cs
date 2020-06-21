using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Infrastructure.Page;
using RayPI.AppService.Comment.Dtos;
using Ray.AppService.AppServices;
using RayPI.Domain.IRepository;

namespace RayPI.AppService.Comment.Queries
{
    public class CommentPageQueryHandler : QueryAppService<Domain.Entity.Comment, CommentDto, CommentDto, Guid, QueryCommentPageDto>,
        IRequestHandler<QueryCommentPageDto, PageResultDto<CommentDto>>
    {
        private readonly IBaseRepository<Domain.Entity.Comment> _commentRepository;

        public CommentPageQueryHandler(IBaseRepository<Domain.Entity.Comment> commentRepository) : base(commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<PageResultDto<CommentDto>> Handle(QueryCommentPageDto request, CancellationToken cancellationToken)
        {
            var result = await this.GetPageAsync(request);
            return result;
        }
    }
}
