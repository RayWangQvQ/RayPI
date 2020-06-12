using System;
using System.Collections.Generic;
using System.Text;
using Ray.Infrastructure.Auditing;

namespace Ray.Domain.Entities.Auditing
{
    /// <summary>
    /// 集成了全部审计的聚合根
    /// </summary>
    public abstract class FullAuditedAggregateRoot : AuditedAggregateRoot, IHasFullAuditing
    {
        /// <inheritdoc />
        public virtual bool IsDeleted { get; set; }

        /// <inheritdoc />
        public virtual Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public virtual DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// 集成了全部审计的聚合根
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class FullAuditedAggregateRoot<TKey> : AuditedAggregateRoot<TKey>, IHasFullAuditing
    {
        /// <inheritdoc />
        public virtual bool IsDeleted { get; set; }

        /// <inheritdoc />
        public virtual Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public virtual DateTime? DeletionTime { get; set; }

        protected FullAuditedAggregateRoot()
        {

        }

        protected FullAuditedAggregateRoot(TKey id)
            : base(id)
        {

        }
    }
}
