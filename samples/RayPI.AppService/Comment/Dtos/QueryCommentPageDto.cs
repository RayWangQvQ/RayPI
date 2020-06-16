using System;
using System.Collections.Generic;
using System.Text;
using Ray.Infrastructure.Page;

namespace RayPI.AppService.Comment.Dtos
{
    public class QueryCommentPageDto : IPageResultRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public Guid ArticleId { get; set; }
    }
}
