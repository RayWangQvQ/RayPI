using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Page
{
    public class PageAndSortRequest : PageRequest, IPageAndSortRequest
    {
        public virtual string Sorting { get; set; }
    }
}
