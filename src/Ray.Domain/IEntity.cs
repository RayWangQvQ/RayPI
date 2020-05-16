using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Domain
{
    /// <summary>
    /// 无主键的实体interface
    /// </summary>
    public interface IEntity
    {
        object[] GetKeys();
    }

    /// <summary>
    /// 有泛型主键的实体interface
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}
