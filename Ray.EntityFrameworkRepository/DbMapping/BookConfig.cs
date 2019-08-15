using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Entity;

namespace RayPI.EntityFrameworkRepository.DbMapping
{
    public class BookConfig : EntityBaseTypeConfig<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            //表名
            builder.ToTable("Book");

            //主键
            builder.HasKey(x => x.Id);

            //字段
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();

            //基础字段
            base.Configure(builder);
        }
    }
}
