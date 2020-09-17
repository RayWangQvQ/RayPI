//系统包
using System;
using System.Threading.Tasks;
//微软包
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
//三方包
using Newtonsoft.Json;
//本地项目包
using RayPI.Infrastructure.Treasury.Models;

namespace RayPI.Infrastructure.RayException.Middleware
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="env"></param>
        public ExceptionMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _next = next;
            _env = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            bool isCatched = false;
            try
            {
                await _next(context);
            }
            //系统抛出的或自己throw的都会进到catch
            //进入到catch后，状态码为200，需要手动赋值
            catch (Exception ex)
            {
                if (ex is RayAppException rayAppException)//自定义业务异常
                {
                    context.Response.StatusCode = rayAppException.Code;
                }
                else//系统异常
                {
                    context.Response.StatusCode = 500;
                    //todo:Log
                }
                await HandleExceptionAsync(context, context.Response.StatusCode, ex.Message);
                isCatched = true;
            }
            finally
            {
                if (!isCatched && context.Response.StatusCode != 200)//未捕捉过并且状态码不为200
                {
                    var msg = context.Response.StatusCode switch
                    {
                        401 => "未授权",
                        404 => "未找到服务",
                        403 => "访问被拒绝",
                        502 => "请求错误",
                        _ => "未知错误",
                    };
                    await HandleExceptionAsync(context, context.Response.StatusCode, msg);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var response = new ApiResponse { Code = statusCode, Msg = msg };
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
