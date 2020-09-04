using System;
using MediatR;

namespace RayPI.Application.ArticleApp.Dtos
{
    public class UpdateArticleDto : CreateArticleDto, IRequest<ArticleDetailDto>
    {
        public Guid Id { get; set; }
    }
}
