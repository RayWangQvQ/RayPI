using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RayPI.AppService.Queries.ViewModels;

namespace RayPI.AppService.Queries
{
    public class ArticleQuery : IRequest<ArticleQueryViewModel>
    {
        public long Id { get; set; }
    }
}
