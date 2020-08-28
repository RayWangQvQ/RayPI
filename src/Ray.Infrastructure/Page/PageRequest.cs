using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ray.Infrastructure.Page
{
    public class PageRequest : IPageRequest
    {
        [Range(1, int.MaxValue)]
        public virtual int PageSize { get; set; } = 10;

        [Range(1, int.MaxValue)]
        public virtual int PageIndex { get; set; } = 1;
    }
}
