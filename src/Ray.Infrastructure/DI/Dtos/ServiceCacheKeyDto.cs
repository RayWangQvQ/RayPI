using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.DI.Dtos
{
    public struct ServiceCacheKeyDto
    {
        public Type Type { get; set; }
        public int Slot { get; set; }
    }
}
