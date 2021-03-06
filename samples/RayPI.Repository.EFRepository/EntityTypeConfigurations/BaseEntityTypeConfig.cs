﻿using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ray.Domain.Entities;
using Ray.Repository.EfCore;

namespace RayPI.Repository.EFRepository.EntityTypeConfigurations
{
    /// <summary>Map实体基类的基础字段</summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseEntityTypeConfig<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : Entity<Guid>
    {
        protected override string TableName => $"App{typeof(TEntity).Name}";

        protected override void ConfigureField(EntityTypeBuilder<TEntity> builder)
        {
            base.ConfigureField(builder);

            builder.HasKey(x => x.Id);
            builder.Property(it => it.Id).IsRequired().ValueGeneratedNever();

            AppConfigure(builder);
        }

        protected abstract void AppConfigure(EntityTypeBuilder<TEntity> builder);
    }
}
