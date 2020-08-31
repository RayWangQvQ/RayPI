using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Entity;

namespace RayPI.Repository.EFRepository.EntityTypeConfigurations
{
    public class CommentConfig : BaseEntityTypeConfig<Comment>
    {
        protected override void AppConfigure(EntityTypeBuilder<Comment> builder)
        {

        }
    }
}
