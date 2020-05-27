using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Infrastructure.Treasury.Helpers;

namespace RayPI.Domain.Entity
{
    public class Article : EntityBase
    {
        public Article()
        {
            this.Id = IdGenerateHelper.NewId;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="content"></param>
        public Article(string title, string subTitle, string content)
        {
            this.Id = IdGenerateHelper.NewId;
            this.Title = title;
            this.SubTitle = subTitle;
            this.Content = content;
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
