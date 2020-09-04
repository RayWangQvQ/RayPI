using System;
using Ray.Application.Dtos;

namespace RayPI.Application.CommentApp.Dtos
{
    public class CommentDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}
