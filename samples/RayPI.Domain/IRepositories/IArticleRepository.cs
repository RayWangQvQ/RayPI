using System;
using Ray.Domain.Repositories;
using RayPI.Domain.Entity;

namespace RayPI.Domain.IRepositories
{
    public interface IArticleRepository:IBaseRepository<Article, Guid>
    {
    }
}
