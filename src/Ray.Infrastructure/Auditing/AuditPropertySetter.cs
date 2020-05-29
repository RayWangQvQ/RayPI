using System;
using System.Collections.Generic;
using System.Text;
using Ray.Infrastructure.Auditing.Creation;
using Ray.Infrastructure.Auditing.Deletion;
using Ray.Infrastructure.Auditing.Modification;
using Ray.Infrastructure.Users;

namespace Ray.Infrastructure.Auditing
{
    public class AuditPropertySetter : IAuditPropertySetter
    {
        protected ICurrentUser CurrentUser { get; }

        public AuditPropertySetter(ICurrentUser currentUser)
        {
            CurrentUser = currentUser;
        }

        /// <summary>
        /// 为创建相关的审计属性赋值
        /// </summary>
        /// <param name="targetObject"></param>
        public void SetCreationProperties(object targetObject)
        {
            SetCreationTime(targetObject);
            SetCreatorId(targetObject);
        }

        /// <summary>
        /// 为修改相关的审计属性赋值
        /// </summary>
        /// <param name="targetObject"></param>
        public void SetModificationProperties(object targetObject)
        {
            SetLastModificationTime(targetObject);
            SetLastModifierId(targetObject);
        }

        /// <summary>
        /// 为删除相关的审计属性赋值
        /// </summary>
        /// <param name="targetObject"></param>
        public void SetDeletionProperties(object targetObject)
        {
            SetDeletionTime(targetObject);
            SetDeleterId(targetObject);
        }



        private void SetCreationTime(object targetObject)
        {
            if (!(targetObject is IHasCreationTime objectWithCreationTime))
            {
                return;
            }

            if (objectWithCreationTime.CreationTime == default)
            {
                objectWithCreationTime.CreationTime = DateTime.Now;
            }
        }

        private void SetCreatorId(object targetObject)
        {
            if (!CurrentUser.Id.HasValue)
            {
                return;
            }

            if (targetObject is IMayHaveCreator mayHaveCreatorObject)
            {
                if (mayHaveCreatorObject.CreatorId.HasValue && mayHaveCreatorObject.CreatorId.Value != default)
                {
                    return;
                }

                mayHaveCreatorObject.CreatorId = CurrentUser.Id;
            }
        }

        private void SetLastModificationTime(object targetObject)
        {
            if (targetObject is IHasModificationTime objectWithModificationTime)
            {
                objectWithModificationTime.LastModificationTime = DateTime.Now;
            }
        }

        private void SetLastModifierId(object targetObject)
        {
            if (!(targetObject is IModificationAuditedObject modificationAuditedObject))
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

        private void SetDeletionTime(object targetObject)
        {
            if (targetObject is IHasDeletionTime objectWithDeletionTime)
            {
                if (objectWithDeletionTime.DeletionTime == null)
                {
                    objectWithDeletionTime.DeletionTime = DateTime.Now;
                }
            }
        }

        private void SetDeleterId(object targetObject)
        {
            if (!(targetObject is IDeletionAuditedObject deletionAuditedObject))
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
    }
}
