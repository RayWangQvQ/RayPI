using RayPI.Treasury.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Auth
{
    public interface IOperateInfo
    {
        /// <summary>登录人信息</summary>
        /// <value>The authentication base.</value>
        TokenModel TokenModel { get; }

        /// <summary>登录token</summary>
        /// <value>The token.</value>
        string TokenStr { get; }
    }
}
