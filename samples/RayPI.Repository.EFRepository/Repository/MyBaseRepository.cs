using System;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Repository.EfCore;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;


namespace RayPI.Repository.EFRepository.Repository
{
    /// <summary>
    /// 仓储层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyBaseRepository<T> : EfRepository<T, Guid>, IMyBaseRepository<T>
        where T : BaseEntity
    {
        public MyBaseRepository(MyDbContext myDbContext, IUnitOfWork unitOfWork) : base(myDbContext, unitOfWork)
        {
        }
    }
}
