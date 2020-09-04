using MediatR;
using Ray.Infrastructure.Page;

namespace RayPI.Application.ArticleApp.Dtos
{
    public class QueryArticlePageDto : PageAndSortRequest, IRequest<PageResultDto<ArticleDetailDto>>
    {
        public string Title { get; set; }
    }
}
