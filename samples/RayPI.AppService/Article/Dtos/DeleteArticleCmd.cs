using System;
using MediatR;

namespace RayPI.AppService.Article.Dtos
{
    public class DeleteArticleCmd : IRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
