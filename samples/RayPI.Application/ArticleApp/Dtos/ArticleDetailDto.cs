using System;
using Ray.Auditing;

namespace RayPI.Application.ArticleApp.Dtos
{
    public class ArticleDetailDto : IHasCreationAuditing, IHasModificationAuditing
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
    }
}
