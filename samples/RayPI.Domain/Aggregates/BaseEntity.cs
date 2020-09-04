using System;
using Ray.Domain.Entities.Auditing;

namespace RayPI.Domain.Aggregates
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class BaseEntity : FullAuditedEntity<Guid>
    {

    }
}
