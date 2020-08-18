using System;
using MediatR;
using Ray.Infrastructure.Page;

namespace RayPI.AppService.CommentApp.Dtos
{
    public class QueryCommentPageDto : IPageResultRequest, IRequest<PageResultDto<CommentDto>>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public Guid? ArticleId { get; set; }
    }
}
