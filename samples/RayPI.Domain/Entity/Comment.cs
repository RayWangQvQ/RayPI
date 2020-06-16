using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Domain.Entity
{
    public class Comment : BaseAggregateRoot
    {
        protected Comment()
        {

        }

        public Comment(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}
