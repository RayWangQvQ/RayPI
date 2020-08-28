namespace Ray.Infrastructure.Page
{
    /// <summary>
    /// 获取分页请求
    /// </summary>
    public interface IPageRequest
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 查询页码
        /// </summary>
        int PageIndex { get; set; }
    }
}
