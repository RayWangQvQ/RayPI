using System;
using MediatR;

namespace RayPI.Application.ArticleApp.Dtos
{
    public class QueryArticleDto : IRequest<ArticleDetailDto>
    {
        public Guid Id { get; set; }
    }
}
