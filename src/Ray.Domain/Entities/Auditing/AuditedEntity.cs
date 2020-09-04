using System;
using System.Collections.Generic;
using System.Text;
using Ray.Auditing;

namespace Ray.Domain.Entities.Auditing
{
    /// <summary>
    /// 集成了创建审计和编辑审计的实体
    /// </summary>
    public abstract class AuditedEntity : Entity, IHasCreationAuditing, IHasModificationAuditing
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
    /// 集成了创建审计和编辑审计的实体
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public abstract class AuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreationAuditing, IHasModificationAuditing
    {
        /// <inheritdoc />
        public virtual DateTime CreationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? CreatorId { get; set; }

        /// <inheritdoc />
        public virtual DateTime? LastModificationTime { get; set; }

        /// <inheritdoc />
        public virtual Guid? LastModifierId { get; set; }

        protected AuditedEntity()
        {

        }

        protected AuditedEntity(TPrimaryKey id)
            : base(id)
        {

        }
    }
}
