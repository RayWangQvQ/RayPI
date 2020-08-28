using System;
using Ray.Application.AppServices;
using RayPI.AppService.CommentApp.Dtos;
using RayPI.Domain.Entity;

namespace RayPI.AppService.CommentApp
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
