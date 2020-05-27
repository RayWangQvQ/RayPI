using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Domain.Entities
{
    /// <summary>
    /// 定义一个聚合根
    /// （聚合根首先必须是一个IEntity实体）
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
    }

    /// <summary>
    /// 定义一个具有单主键Id的聚合根
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>, IAggregateRoot
    {

    }
}
