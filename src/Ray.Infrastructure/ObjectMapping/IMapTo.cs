using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.ObjectMapping
{
    public interface IMapTo<TDestination>
    {
        TDestination MapTo();

        void MapTo(TDestination destination);
    }
}
