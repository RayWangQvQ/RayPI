namespace RayPI.Infrastructure.Security.Models
{
    /// <summary>
    /// 标准Token响应
    /// </summary>
    public class ResponseToken
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Access_Token { get; set; }

        /// <summary>
        /// 有效时间(秒)
        /// </summary>
        public double Expires_In { get; set; }

        /// <summary>
        /// Token类型(使用jwt)
        /// </summary>
        public string Token_Type { get; set; }
    }
}
