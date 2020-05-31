using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ray.Domain.Repositories;
using Ray.Infrastructure.Repository.EfCore;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Treasury.Enums;
using RayPI.Infrastructure.Treasury.Models;


namespace RayPI.Repository.EFRepository.Repository
{
    /// <summary>
    /// 仓储层基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : EfRepository<T, Guid, MyDbContext>, IBaseRepository<T>
        where T : BaseEntity
    {
        public BaseRepository(MyDbContext myDbContext) : base(myDbContext)
        {
        }
    }
}
