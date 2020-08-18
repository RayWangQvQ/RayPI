using System;

namespace RayPI.AppService.ArticleApp.Dtos
{
    public class UpdateArticleDto : CreateArticleDto
    {
        public Guid Id { get; set; }
    }
}
