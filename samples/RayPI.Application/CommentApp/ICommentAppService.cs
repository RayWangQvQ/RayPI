using System;
using Ray.Application.IAppServices;
using RayPI.AppService.CommentApp.Dtos;

namespace RayPI.AppService.CommentApp
{
    /// <summary>
    /// 评论AppService
    /// </summary>
    public interface ICommentAppService
        : ICrudAppService<Guid, CommentDto>
    {
    }
}
