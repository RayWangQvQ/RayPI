using Ray.Domain;
using RayPI.Domain.Events;

namespace RayPI.Domain.Aggregates.ArticleAggregate
{
    public class Article : BaseAggregateRoot
    {
        protected Article()
        {

        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="title"></param>
        public Article(string title)
        {
            this.Title = title;
            AddDomainEvent(new ArticleAddedDomainEvent(this));
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
