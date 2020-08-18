using System;
using MediatR;

namespace RayPI.AppService.ArticleApp.Dtos
{
    public class DeleteArticleDto : IRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
