using System;
using MediatR;

namespace RayPI.Application.ArticleApp.Dtos
{
    public class DeleteArticleDto : IRequest<bool>
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
