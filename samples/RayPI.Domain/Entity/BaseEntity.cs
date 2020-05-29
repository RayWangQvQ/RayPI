using System;
using Ray.Domain;
using Ray.Domain.Entities;
using Ray.Domain.Entities.Auditing;
using RayPI.Infrastructure.Treasury.Interfaces;

namespace RayPI.Domain.Entity
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class BaseEntity : FullAuditedEntity<Guid>
    {

    }
}
