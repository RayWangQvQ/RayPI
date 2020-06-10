using System;

namespace RayPI.AppService.Article.Dtos
{
    public class UpdateArticleCmd : CreateArticleCmd
    {
        public Guid Id { get; set; }
    }
}
