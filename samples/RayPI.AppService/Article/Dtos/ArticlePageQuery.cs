using System.Collections.Generic;
using MediatR;

namespace RayPI.AppService.Article.Dtos
{
    public class ArticlePageQuery : IRequest<List<ArticleQueryViewModel>>
    {
        public string Title { get; set; }
    }
}
