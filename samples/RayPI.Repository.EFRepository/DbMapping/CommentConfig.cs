using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Entity;

namespace RayPI.Repository.EFRepository.DbMapping
{
    public class CommentConfig : EntityBaseTypeConfig<Comment>
    {
        protected override void AppConfigure(EntityTypeBuilder<Comment> builder)
        {

        }
    }
}
