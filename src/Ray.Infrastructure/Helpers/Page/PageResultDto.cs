using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Helpers.Page
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResultDto<T>
    {
        /// <summary>
        /// 内容列表
        /// </summary>
        public List<T> List { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 分页长度
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
    }
}
