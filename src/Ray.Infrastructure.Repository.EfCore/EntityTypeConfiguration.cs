using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ray.Domain.Entities;
using Ray.Infrastructure.Auditing.Deletion;
using Ray.Infrastructure.Repository.EfCore.Extensions;

namespace Ray.Infrastructure.Repository.EfCore
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

            //查询时的全局过滤
            ConfigureQueryFilter(builder);
        }

        /// <summary>
        /// 实体对应数据库的表名
        /// （默认为实体类名称）
        /// </summary>
        protected virtual string TableName => typeof(TEntity).Name;

        /// <summary>
        /// 字段的配置映射
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void ConfigureField(EntityTypeBuilder<TEntity> builder)
        {
            builder.TryConfigureBasic();
        }

        /// <summary>
        /// 默认查询时的全局过滤
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void ConfigureQueryFilter(EntityTypeBuilder<TEntity> builder)
        {
            Expression<Func<TEntity, bool>> expression = null;
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                expression = e => !EF.Property<bool>(e, "IsDeleted");
            }

            builder.HasQueryFilter(expression);
        }
    }
}
