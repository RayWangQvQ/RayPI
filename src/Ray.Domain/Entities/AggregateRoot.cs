using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Domain.Entities
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        protected AggregateRoot()
        {

        }
    }

    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
        protected AggregateRoot()
        {

        }

        protected AggregateRoot(TKey id)
            : base(id)
        {

        }
    }
}
