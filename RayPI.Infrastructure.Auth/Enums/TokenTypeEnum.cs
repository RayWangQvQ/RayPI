using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Auth.Enums
{
    /// <summary>
    /// 终端类型枚举
    /// </summary>
    public enum TokenTypeEnum
    {
        Web,//网页终端
        App,//移动终端
        MiniProgram,//小程序终端
        Other,//其他类型终端
    }
}
