using System;
using System.Threading.Tasks;
using Ray.Application.AppServices;
using Ray.Infrastructure.Page;
using RayPI.AppService.CommentApp.Dtos;
using RayPI.Domain.IRepository;
using RayPI.Domain.Entity;

namespace RayPI.AppService.CommentApp
{
    public class CommentAppService : CrudAppService<Comment, CommentDto, CommentDto, Guid, QueryCommentPageDto, CommentDto, CommentDto>, ICommentAppService
    {
        private readonly IBaseRepository<Comment> _commentRepository;

        public CommentAppService(IBaseRepository<Comment> commentRepository) : base(commentRepository)
        {
            _commentRepository = commentRepository;
        }
    }
}
