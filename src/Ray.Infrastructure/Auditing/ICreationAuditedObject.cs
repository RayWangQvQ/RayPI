﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Auditing
{
    /// <summary>
    /// 定义了创建信息属性 (创建人与创建时间).
    /// </summary>
    public interface ICreationAuditedObject : IHasCreationTime, IMayHaveCreator
    {

    }

    /// <summary>
    /// 定义了创建信息属性 (创建人与创建时间).
    /// </summary>
    /// <typeparam name="TCreator">Type of the user</typeparam>
    public interface ICreationAuditedObject<TCreator> : ICreationAuditedObject, IMayHaveCreator<TCreator>
    {

    }
}
