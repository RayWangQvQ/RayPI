namespace Ray.Infrastructure.Page
{
    public interface IPageResultRequest
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
