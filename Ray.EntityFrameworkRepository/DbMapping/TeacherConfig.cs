using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Domain.Entity;

namespace RayPI.EntityFrameworkRepository.DbMapping
{
    public class TeacherConfig : EntityBaseTypeConfig<TeacherEntity>
    {
        public override void Configure(EntityTypeBuilder<TeacherEntity> builder)
        {
            //表名
            builder.ToTable("Teacher");

            //字段
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            //基础字段
            base.Configure(builder);
        }
    }
}
