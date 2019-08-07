using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Treasury.Models
{
    public class TokenModel
    {
        /// <summary>
        /// 登录用户Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// 令牌类型
        /// </summary>
        public string TokenType { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string TokenString { get; set; }
    }
}
