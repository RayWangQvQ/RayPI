using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ray.Domain.Events
{
    public class DomianEvent<TEventData> : IDomainEvent
    {
        public TEventData Data { get; set; }

        public DomianEvent(TEventData eventData)
        {
            Data = eventData;
        }
    }
}
