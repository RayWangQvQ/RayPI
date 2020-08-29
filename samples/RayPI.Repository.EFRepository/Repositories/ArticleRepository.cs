using System;
using Ray.Domain.Repositories;
using Ray.Repository.EfCore;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepositories;

namespace RayPI.Repository.EFRepository.Repositories
{
    public class ArticleRepository : EfRepository<Article, Guid>, IArticleRepository
    {
        public ArticleRepository(MyDbContext myDbContext, IUnitOfWork unitOfWork) : base(myDbContext, unitOfWork)
        {
        }
    }
}
