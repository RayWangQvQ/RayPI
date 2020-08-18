using System.Collections.Generic;
using MediatR;

namespace RayPI.AppService.ArticleApp.Dtos
{
    public class QueryArticlePageDto : IRequest<List<ResponseQueryArticleDto>>
    {
        public string Title { get; set; }
    }
}
