using System;
using System.Collections.Generic;
using System.Text;
using Ray.Auditing;

namespace Ray.Domain.Entities.Auditing
{
    /// <summary>
    /// 集成了全部审计的实体
    /// </summary>
    public abstract class FullAuditedEntity : AuditedEntity, IHasFullAuditing
    {
        /// <inheritdoc />
        public virtual bool IsDeleted { get; set; }

        /// <inheritdoc />
        public virtual Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public virtual DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// 集成了全部审计的实体
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IHasFullAuditing
    {
        /// <inheritdoc />
        public virtual bool IsDeleted { get; set; }

        /// <inheritdoc />
        public virtual Guid? DeleterId { get; set; }

        /// <inheritdoc />
        public virtual DateTime? DeletionTime { get; set; }

        protected FullAuditedEntity()
        {

        }

        protected FullAuditedEntity(TPrimaryKey id)
            : base(id)
        {

        }
    }
}
