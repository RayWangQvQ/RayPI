using System;
using System.Threading.Tasks;
using Ray.Application.AppServices;
using Ray.Infrastructure.Page;
using RayPI.AppService.CommentApp.Dtos;
using RayPI.Domain.IRepository;
using RayPI.Domain.Entity;
using Ray.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace RayPI.AppService.CommentApp
{
    public class CommentAppService : CrudAppService<Comment, Guid, QueryCommentPageDto, CommentDto>, ICommentAppService
    {
        public CommentAppService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        //protected override IRepositoryBase<Comment, Guid> Repository => this.ServiceProvider.GetRequiredService<IBaseRepository<Comment>>();
    }
}
