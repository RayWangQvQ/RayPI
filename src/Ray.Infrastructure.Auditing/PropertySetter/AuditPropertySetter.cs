using System;
using Ray.Infrastructure.Security.User;

namespace Ray.Infrastructure.Auditing.PropertySetter
{
    /// <summary>
    /// 实体审计属性赋值器
    /// </summary>
    public class AuditPropertySetter : IAuditPropertySetter
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        protected ICurrentUser CurrentUser { get; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="currentUser"></param>
        public AuditPropertySetter(ICurrentUser currentUser)
        {
            CurrentUser = currentUser;
        }

        public void SetCreationProperties(object targetObject)
        {
            SetCreationTime(targetObject);
            SetCreatorId(targetObject);
        }

        public void SetModificationProperties(object targetObject)
        {
            SetLastModificationTime(targetObject);
            SetLastModifierId(targetObject);
        }

        public void SetDeletionProperties(object targetObject)
        {
            SetDeletionTime(targetObject);
            SetDeleterId(targetObject);
        }


        #region 私有
        /// <summary>
        /// 赋值创建时间
        /// </summary>
        /// <param name="targetObject"></param>
        private void SetCreationTime(object targetObject)
        {
            if (!(targetObject is IHasCreationAuditing objectWithCreationTime))
            {
                return;
            }

            if (objectWithCreationTime.CreationTime == default)
            {
                objectWithCreationTime.CreationTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 赋值创建人Id
        /// </summary>
        /// <param name="targetObject"></param>
        private void SetCreatorId(object targetObject)
        {
            if (!CurrentUser.Id.HasValue)
            {
                return;
            }

            if (targetObject is IHasCreationAuditing mayHaveCreatorObject)
            {
                if (mayHaveCreatorObject.CreatorId.HasValue && mayHaveCreatorObject.CreatorId.Value != default)
                {
                    return;
                }

                mayHaveCreatorObject.CreatorId = CurrentUser.Id;
            }
        }

        /// <summary>
        /// 赋值最后编辑时间
        /// </summary>
        /// <param name="targetObject"></param>
        private void SetLastModificationTime(object targetObject)
        {
            if (targetObject is IHasModificationAuditing objectWithModificationTime)
            {
                objectWithModificationTime.LastModificationTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 赋值最后编辑人Id
        /// </summary>
        /// <param name="targetObject"></param>
        private void SetLastModifierId(object targetObject)
        {
            if (!(targetObject is IHasModificationAuditing modificationAuditedObject))
            {
                return;
            }

            if (!CurrentUser.Id.HasValue)
            {
                modificationAuditedObject.LastModifierId = null;
                return;
            }

            modificationAuditedObject.LastModifierId = CurrentUser.Id;
        }

        /// <summary>
        /// 赋值逻辑删除时间
        /// </summary>
        /// <param name="targetObject"></param>
        private void SetDeletionTime(object targetObject)
        {
            if (targetObject is IHasDeletionAuditing objectWithDeletionTime)
            {
                if (objectWithDeletionTime.DeletionTime == null)
                {
                    objectWithDeletionTime.DeletionTime = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// 赋值逻辑删除人Id
        /// </summary>
        /// <param name="targetObject"></param>
        private void SetDeleterId(object targetObject)
        {
            if (!(targetObject is IHasDeletionAuditing deletionAuditedObject))
            {
                return;
            }

            if (deletionAuditedObject.DeleterId != null)
            {
                return;
            }

            if (!CurrentUser.Id.HasValue)
            {
                deletionAuditedObject.DeleterId = null;
                return;
            }

            deletionAuditedObject.DeleterId = CurrentUser.Id;
        }
        #endregion
    }
}
