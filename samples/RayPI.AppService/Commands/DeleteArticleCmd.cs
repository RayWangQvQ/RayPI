using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RayPI.AppService.Commands
{
    public class DeleteArticleCmd : IRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
