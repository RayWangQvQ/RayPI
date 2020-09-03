using System;
using Ray.Application.Dtos;
using Ray.Infrastructure.Auditing;

namespace RayPI.AppService.CommentApp.Dtos
{
    public class CommentDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}
