using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Ray.Infrastructure.Repository.EfCore;

namespace RayPI.Repository.EFRepository.DbMapping
{
    /// <summary>Map实体基类的基础字段</summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityBaseTypeConfig<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public override void ConfigureField(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(it => it.Id).IsRequired().ValueGeneratedNever();

            MyConfigureField(builder);

            builder.Property(x => x.CreatorId).IsRequired(false);
            //builder.Property(x => x.CreateName).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.CreationTime).IsRequired();

            builder.Property(x => x.LastModifierId).IsRequired(false);
            //builder.Property(x => x.UpdateName).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.LastModificationTime).IsRequired(false);

            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DeleterId).IsRequired(false);
            builder.Property(x => x.DeletionTime).IsRequired(false);
        }

        public abstract void MyConfigureField(EntityTypeBuilder<TEntity> builder);
    }
}
