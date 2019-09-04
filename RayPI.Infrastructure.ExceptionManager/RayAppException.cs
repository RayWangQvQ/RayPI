//系统包
using System;

namespace RayPI.Infrastructure.RayException
{
    /// <summary>
    /// 自定义业务异常类
    /// </summary>
    [Serializable]
    public class RayAppException : ApplicationException
    {

        /// <summary>
        /// 
        /// </summary>
        public RayAppException()
        {
            Code = 500;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public RayAppException(string message, int code = 500)
            : base(message)
        {
            Code = code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <param name="code"></param>
        public RayAppException(string message, Exception inner, int code = 500)
            : base(message, inner)
        {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }
    }
}
