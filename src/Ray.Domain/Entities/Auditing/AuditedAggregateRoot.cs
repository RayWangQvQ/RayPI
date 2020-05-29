using System;
using System.Collections.Generic;
using System.Text;
using Ray.Infrastructure.Auditing;

namespace Ray.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAuditedObject"/> for aggregate roots.
    /// </summary>
    public abstract class AuditedAggregateRoot : AggregateRoot, IAuditedObject
    {
        /// <inheritdoc />
        public virtual DateTime CreationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? CreatorId { get; set; }

        /// <inheritdoc />
        public virtual DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? LastModifierId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAuditedObject"/> for aggregate roots.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public abstract class AuditedAggregateRoot<TPrimaryKey> : AggregateRoot<TPrimaryKey>, IAuditedObject
    {
        /// <inheritdoc />
        public virtual DateTime CreationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? CreatorId { get; set; }

        /// <inheritdoc />
        public virtual DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? LastModifierId { get; set; }

        protected AuditedAggregateRoot()
        {

        }

        protected AuditedAggregateRoot(TPrimaryKey id)
            : base(id)
        {

        }
    }
}
