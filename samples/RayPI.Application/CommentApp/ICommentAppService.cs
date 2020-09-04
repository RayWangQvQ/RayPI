using System;
using Ray.Application.IAppServices;
using RayPI.Application.CommentApp.Dtos;

namespace RayPI.Application.CommentApp
{
    /// <summary>
    /// 评论AppService
    /// </summary>
    public interface ICommentAppService
        : ICrudAppService<Guid, CommentDto>
    {
    }
}
