using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ray.EventBus.Abstractions
{
    /// <summary>
    /// 综合事件处理器
    /// </summary>
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
