﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ray.Application.AppServices;
using RayPI.Application.ArticleApp.Dtos;
using RayPI.Domain.Aggregates.ArticleAggregate;

namespace RayPI.Application.ArticleApp.Queries
{
    public class ArticleQueryHandler
        : QueryAppService<Article, Guid, ArticleDetailDto>,
            IRequestHandler<QueryArticleDto, ArticleDetailDto>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ArticleQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ArticleDetailDto> Handle(QueryArticleDto request, CancellationToken cancellationToken)
        {
            return await GetAsync(request.Id);
        }
    }
}
