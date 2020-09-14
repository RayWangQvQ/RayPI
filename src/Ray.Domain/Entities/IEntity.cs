using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Domain.Entities
{
    /// <summary>
    /// 定义一个类为【实体】
    /// 它的主键不一定是Id，甚至可能是复合主键。
    /// 建议使用集成更好的带Id主键的 <see cref="IEntity{TKey}"/>.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 返回实体的key集合
        /// </summary>
        /// <returns></returns>
        object[] GetKeys();

        void AddCreatedDomainEvent();
    }

    /// <summary>
    /// 定义一个以泛型Id为单主键的【实体】
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键Type</typeparam>
    public interface IEntity<TPrimaryKey> : IEntity
    {
        /// <summary>
        /// 实体主键，作为唯一标识
        /// </summary>
        TPrimaryKey Id { get; }
    }
}
