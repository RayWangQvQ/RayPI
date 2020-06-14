using System;

namespace RayPI.AppService.Article.Dtos
{
    public class UpdateArticleDto : CreateArticleDto
    {
        public Guid Id { get; set; }
    }
}
