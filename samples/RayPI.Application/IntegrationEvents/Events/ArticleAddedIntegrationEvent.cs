using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Application.ArticleApp.Dtos;
using RayPI.Domain.Aggregates.ArticleAggregate;

namespace RayPI.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 【文章新增】领域事件
    /// </summary>
    public class ArticleAddedIntegrationEvent
    {
        public ArticleAddedIntegrationEvent(ArticleDetailDto dto)
        {
            ArticleDto = dto;
        }

        public ArticleDetailDto ArticleDto { get; set; }
    }
}
