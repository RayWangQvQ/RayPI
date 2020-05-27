using System;
using System.Collections.Generic;
using System.Text;
using Ray.Infrastructure.Auditing;

namespace Ray.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAuditedObject"/> for aggregate roots.
    /// </summary>
    [Serializable]
    public abstract class AuditedAggregateRoot : CreationAuditedAggregateRoot, IAuditedObject
    {
        /// <inheritdoc />
        public virtual DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? LastModifierId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAuditedObject"/> for aggregate roots.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    [Serializable]
    public abstract class AuditedAggregateRoot<TKey> : CreationAuditedAggregateRoot<TKey>, IAuditedObject
    {
        /// <inheritdoc />
        public virtual DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? LastModifierId { get; set; }

        protected AuditedAggregateRoot()
        {

        }
    }
}
