using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Ray.Infrastructure.Page;

namespace RayPI.AppService.CommentApp.Dtos
{
    public class QueryCommentPageDto : IPageResultRequest, IRequest<PageResultDto<CommentDto>>
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int PageSize { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int PageIndex { get; set; }

        public Guid? ArticleId { get; set; }
    }
}
