using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.EntityFrameworkRepository.DbMapping
{
    public class StudentConfig : EntityBaseTypeConfig<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            base.Configure(builder);

            builder.ToTable("Student");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        }
    }
}
