using System;
using MediatR;

namespace RayPI.AppService.ArticleApp.Dtos
{
    public class UpdateArticleDto : CreateArticleDto, IRequest<ArticleDetailDto>
    {
        public Guid Id { get; set; }
    }
}
