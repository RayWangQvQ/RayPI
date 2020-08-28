using System;
using MediatR;

namespace RayPI.AppService.ArticleApp.Dtos
{
    public class QueryArticleDto : IRequest<ResponseQueryArticleDto>
    {
        public Guid Id { get; set; }
    }
}
