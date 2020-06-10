using System;
using MediatR;

namespace RayPI.AppService.Article.Dtos
{
    public class ArticleQuery : IRequest<ArticleQueryViewModel>
    {
        public Guid Id { get; set; }
    }
}
