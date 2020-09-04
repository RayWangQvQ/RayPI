using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RayPI.Domain.Aggregates.ArticleAggregate;

namespace RayPI.Repository.EFRepository.EntityTypeConfigurations
{
    public class ArticleConfig : BaseEntityTypeConfig<Article>
    {
        protected override void AppConfigure(EntityTypeBuilder<Article> builder)
        {
            //map：
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.SubTitle);
            builder.Property(x => x.Content);

            //初始化数据
            builder.HasData(new Article("这是一条初始化的数据")
            {
                SubTitle = "来自DbContext的OnModelCreating",
                Content = "这是内容",
                CreationTime = DateTime.Now,
            });
        }
    }
}
