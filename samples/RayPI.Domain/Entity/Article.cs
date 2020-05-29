using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Infrastructure.Treasury.Helpers;

namespace RayPI.Domain.Entity
{
    public class Article : BaseEntity
    {
        public Article()
        {

        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="title"></param>
        public Article(string title)
        {
            this.Title = title;
        }

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
