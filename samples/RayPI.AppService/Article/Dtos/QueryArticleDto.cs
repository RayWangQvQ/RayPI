using System;
using MediatR;

namespace RayPI.AppService.Article.Dtos
{
    public class QueryArticleDto : IRequest<ResponseQueryArticleDto>
    {
        public Guid Id { get; set; }
    }
}
