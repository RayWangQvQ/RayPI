using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Ray.Domain;
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
