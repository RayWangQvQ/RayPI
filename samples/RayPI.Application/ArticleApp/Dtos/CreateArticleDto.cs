using System;
using MediatR;

namespace RayPI.AppService.ArticleApp.Dtos
{
    public class CreateArticleDto : IRequest<ArticleDetailDto>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
