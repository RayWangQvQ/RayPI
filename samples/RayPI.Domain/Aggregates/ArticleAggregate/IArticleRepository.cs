using System;
using Ray.Domain.Repositories;

namespace RayPI.Domain.Aggregates.ArticleAggregate
{
    public interface IArticleRepository:IBaseRepository<Article, Guid>
    {
    }
}
