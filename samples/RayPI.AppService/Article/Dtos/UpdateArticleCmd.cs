using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.AppService.Commands
{
    public class UpdateArticleCmd : CreateArticleCmd
    {
        public Guid Id { get; set; }
    }
}
