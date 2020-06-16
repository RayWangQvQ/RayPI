using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.ObjectMapping
{
    public interface IMapFrom<in TSource>
    {
        void MapFrom(TSource source);
    }
}
