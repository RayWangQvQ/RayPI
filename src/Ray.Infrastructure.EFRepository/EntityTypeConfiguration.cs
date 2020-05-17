using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ray.Domain;

namespace Ray.Infrastructure.EFRepository
{
    /// <summary>
    /// 实体与数据库表的映射关系
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// 模板方法
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //数据库表名
            builder.ToTable(TableName);

            //字段的配置映射
            ConfigureField(builder);
        }

        /// <summary>
        /// 实体对应数据库的表名
        /// （默认为实体类名称）
        /// </summary>
        public virtual string TableName => typeof(TEntity).Name;

        /// <summary>
        /// 字段的配置映射
        /// </summary>
        /// <param name="builder"></param>
        public abstract void ConfigureField(EntityTypeBuilder<TEntity> builder);
    }
}
