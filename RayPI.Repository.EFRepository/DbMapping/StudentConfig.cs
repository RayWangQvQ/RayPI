using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Domain.Entity;

namespace RayPI.Repository.EFRepository.DbMapping
{
    public class StudentConfig : EntityBaseTypeConfig<StudentEntity>
    {
        public override void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
            //表名
            builder.ToTable("Student");

            //字段
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            //基础字段
            base.Configure(builder);
        }
    }
}
