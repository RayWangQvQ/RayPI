//微软包
using Microsoft.AspNetCore.Builder;
//本地项目包
using RayPI.Infrastructure.ExceptionManager.Middleware;

namespace RayPI.Infrastructure.ExceptionManager.Di
{
    public static class ExceptionDiExtension
    {
        public static void UseExceptionService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();//自定义异常处理中间件
        }
    }
}
