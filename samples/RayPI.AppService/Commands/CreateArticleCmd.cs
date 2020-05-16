using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RayPI.AppService.Commands
{
    public class CreateArticleCmd : IRequest<long>
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
