using System.Collections.Generic;
using MediatR;
using Ray.Infrastructure.Page;

namespace RayPI.AppService.ArticleApp.Dtos
{
    public class QueryArticlePageDto : PageAndSortRequest, IRequest<PageResultDto<ArticleDetailDto>>
    {
        public string Title { get; set; }
    }
}
