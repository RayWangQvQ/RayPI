using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Application.ArticleApp.Dtos;
using RayPI.Domain.Aggregates.ArticleAggregate;

namespace RayPI.Application.IntegrationEvents.Events
{
    public class ArticleAddedIntegrationEvent
    {
        public ArticleAddedIntegrationEvent(Guid articleId)
        {
            ArticleId = articleId;
        }

        public Guid ArticleId { get; }
    }
}
