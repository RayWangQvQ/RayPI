using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ray.Domain.Repositories;
using RayPI.Domain.Entity;
using RayPI.Infrastructure.Treasury.Enums;
using RayPI.Infrastructure.Treasury.Models;


namespace RayPI.Domain.IRepository
{
    public interface IBaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {

    }
}
