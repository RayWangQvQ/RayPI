using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        /// <summary>
        /// 事件Id
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationDate { get; private set; }
    }
}
