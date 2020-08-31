using System;
using MediatR;

namespace RayPI.AppService.ArticleApp.Dtos
{
    public class QueryArticleDto : IRequest<ArticleDetailDto>
    {
        public Guid Id { get; set; }
    }
}
