using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Domain.Events
{
    public class EntityCreatedDomainEvent<TEventData> : DomianEvent<TEventData>
    {
        public EntityCreatedDomainEvent(TEventData eventData) : base(eventData)
        {
        }
    }
}
