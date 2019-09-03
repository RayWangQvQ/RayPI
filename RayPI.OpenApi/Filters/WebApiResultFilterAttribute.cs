//微软包
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
//本地项目包
using RayPI.Infrastructure.Treasury.Models;

namespace RayPI.OpenApi.Filters
{
    /// <summary>
    /// 接口返回统一格式过滤器
    /// </summary>
    public class WebApiResultFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 包装接口返回类型
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            switch (context.Result)
            {
                //根据实际需求进行具体实现
                case ObjectResult objectResult:
                    context.Result = objectResult.Value == null
                        ? new ObjectResult(new ApiResponse { Code = 404, Msg = "未找到资源" })
                        : new ObjectResult(new ApiResponse { Code = 200, Msg = "", Data = objectResult.Value });
                    break;
                case EmptyResult emptyResult:
                    context.Result = new ObjectResult(new ApiResponse { Code = 404, Msg = "未找到资源" });
                    break;
                case ContentResult contentResult:
                    context.Result = new ObjectResult(new ApiResponse { Code = 200, Msg = "", Data = contentResult.Content });
                    break;
                case StatusCodeResult statusResult:
                    context.Result = new ObjectResult(new ApiResponse { Code = statusResult.StatusCode, Msg = "" });
                    break;
            }
        }
    }
}
