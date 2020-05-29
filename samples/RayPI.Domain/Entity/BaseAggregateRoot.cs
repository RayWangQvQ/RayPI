using System;
using System.Collections.Generic;
using System.Text;
using Ray.Domain.Entities.Auditing;

namespace RayPI.Domain.Entity
{
    public abstract class BaseAggregateRoot : FullAuditedAggregateRoot<Guid>
    {
    }
}
