using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ray.Infrastructure.Auditing;
using Ray.Infrastructure.Extensions;

namespace Ray.Repository.EfCore.Extensions
{
    public static class EntityTypeBuilderExtension
    {
        public static void TryConfigureBasicProperties(this EntityTypeBuilder b)
        {
            b.TryConfigureLogicDelete();

            b.TryConfigureCreationAuditing();
            b.TryConfigureLastModificationAuditing();
            b.TryConfigureDeletionAuditing();
        }

        /// <summary>
        /// 尝试配置逻辑删除属性
        /// </summary>
        /// <param name="b"></param>
        public static void TryConfigureLogicDelete(this EntityTypeBuilder b)
        {
            if (b.Metadata.ClrType.IsAssignableTo<ILogicDeletable>())
            {
                b.Property(nameof(ILogicDeletable.IsDeleted))
                    .IsRequired()
                    .HasDefaultValue(false)
                    .HasColumnName(nameof(ILogicDeletable.IsDeleted));
            }
        }

        /// <summary>
        /// 尝试配置创建相关审计属性
        /// </summary>
        /// <param name="b"></param>
        public static void TryConfigureCreationAuditing(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<IHasCreationAuditing>()) return;

            //创建人Id：
            b.Property(nameof(IHasCreationAuditing.CreatorId))
                .IsRequired(false)
                .HasColumnName(nameof(IHasCreationAuditing.CreatorId));

            //创建时间：
            b.Property(nameof(IHasCreationAuditing.CreationTime))
                .IsRequired()
                .HasColumnName(nameof(IHasCreationAuditing.CreationTime));
        }

        /// <summary>
        /// 尝试配置最后编辑相关的审计属性
        /// </summary>
        /// <param name="b"></param>
        public static void TryConfigureLastModificationAuditing(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<IHasModificationAuditing>()) return;

            //最后编辑人Id：
            b.Property(nameof(IHasModificationAuditing.LastModifierId))
                .IsRequired(false)
                .HasColumnName(nameof(IHasModificationAuditing.LastModifierId));

            //最后编辑时间
            b.Property(nameof(IHasModificationAuditing.LastModificationTime))
                .IsRequired(false)
                .HasColumnName(nameof(IHasModificationAuditing.LastModificationTime));
        }

        /// <summary>
        /// 尝试配置删除相关的审计属性
        /// </summary>
        /// <param name="b"></param>
        public static void TryConfigureDeletionAuditing(this EntityTypeBuilder b)
        {
            if (b.Metadata.ClrType.IsAssignableTo<ILogicDeletable>())
            {
                b.TryConfigureLogicDelete();

                b.Property(nameof(IHasDeletionAuditing.DeleterId))
                    .IsRequired(false)
                    .HasColumnName(nameof(IHasDeletionAuditing.DeleterId));

                b.Property(nameof(IHasDeletionAuditing.DeletionTime))
                    .IsRequired(false)
                    .HasColumnName(nameof(IHasDeletionAuditing.DeletionTime));
            }
        }
    }
}
