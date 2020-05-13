using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Domain.Entity
{
    public class ArticleEntity : EntityBase
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
