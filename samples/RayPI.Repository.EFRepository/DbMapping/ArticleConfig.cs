using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Entity;

namespace RayPI.Repository.EFRepository.DbMapping
{
    public class ArticleConfig : EntityBaseTypeConfig<Article>
    {
        public override void MyConfigureField(EntityTypeBuilder<Article> builder)
        {
            //map：
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.SubTitle);
            builder.Property(x => x.Content);
        }
    }
}
