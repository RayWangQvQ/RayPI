using System;
using System.Collections.Generic;
using System.Text;
using Ray.Infrastructure.Auditing;

namespace Ray.Domain.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAuditedObject"/>.
    /// </summary>
    public abstract class AuditedEntity : CreationAuditedEntity, IAuditedObject
    {
        /// <inheritdoc />
        public virtual DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? LastModifierId { get; set; }
    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAuditedObject"/>.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public abstract class AuditedEntity<TKey> : CreationAuditedEntity<TKey>, IAuditedObject
    {
        /// <inheritdoc />
        public virtual DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? LastModifierId { get; set; }

        protected AuditedEntity()
        {

        }

        protected AuditedEntity(TKey id)
            : base(id)
        {

        }
    }
}
