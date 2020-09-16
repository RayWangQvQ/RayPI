using MediatR;
using Ray.Domain.Events;
using RayPI.Domain.Aggregates.ArticleAggregate;

namespace RayPI.Domain.Events
{
    /// <summary>
    /// 文章新增领域事件
    /// </summary>
    public class ArticleAddedDomainEvent : IDomainEvent, INotification
    {
        public Article Article { get; }

        public ArticleAddedDomainEvent(Article article)
        {
            Article = article;
        }
    }
}
