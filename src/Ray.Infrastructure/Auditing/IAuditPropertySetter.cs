using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Auditing
{
    public interface IAuditPropertySetter
    {
        void SetCreationProperties(object targetObject);

        void SetModificationProperties(object targetObject);

        void SetDeletionProperties(object targetObject);
    }
}
