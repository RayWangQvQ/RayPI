using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Entity;

namespace RayPI.Repository.EFRepository.DbMapping
{
    public class ArticleConfig : EntityBaseTypeConfig<ArticleEntity>
    {
        public override void Configure(EntityTypeBuilder<ArticleEntity> builder)
        {
            //表名
            builder.ToTable("Article");

            //字段
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.SubTitle);
            builder.Property(x => x.Content);

            //基础字段
            base.Configure(builder);
        }
    }
}
