using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ray.Infrastructure.EFRepository;
using RayPI.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Repository.EFRepository.DbMapping
{
    /// <summary>Map实体基类的基础字段</summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityBaseTypeConfig<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : EntityBase
    {
        public override void ConfigureField(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(it => it.Id).IsRequired().ValueGeneratedNever();

            MyConfigureField(builder);

            builder.Property(x => x.CreateId).IsRequired(false);
            builder.Property(x => x.CreateName).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.CreateTime).IsRequired(false);
            builder.Property(x => x.UpdateId).IsRequired(false);
            builder.Property(x => x.UpdateName).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.UpdateTime).IsRequired(false);

            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeleteTime).IsRequired(false);

            builder.Ignore(x => x.AutoSetter);
        }

        public abstract void MyConfigureField(EntityTypeBuilder<TEntity> builder);
    }
}
