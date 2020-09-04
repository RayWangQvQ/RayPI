using System;
using Ray.Application.AppServices;
using RayPI.Application.CommentApp.Dtos;
using RayPI.Domain.Aggregates.CommentAggregate;

namespace RayPI.Application.CommentApp
{
    /// <summary>
    /// 评论AppService
    /// </summary>
    public class CommentAppService : CrudAppService<Comment, Guid, CommentDto>, ICommentAppService
    {
        public CommentAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
