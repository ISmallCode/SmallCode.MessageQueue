using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Middlewares
{
    /// <summary>
    /// 自定义日志保存到数据库的插件，当访问出现问题也可以记录exception
    /// </summary>
    public static class TraceLogExtension
    {
        public static IApplicationBuilder UseTraceLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TraceLog>();
        }
    }

    public class TraceLog
    {
        private readonly RequestDelegate next;
        public TraceLog(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context, ILogService logService)
        {
            string ip = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
                string url = context.Request.Path.Value;
                Log log = new Log()
                {
                    Description = "访问了" + url,
                    Level = Level.记录,
                    Ip = ip,
                };
                logService.Save(log);
               await next(context);
            }
            catch (Exception ex)
            {
                Log log = new Log()
                {
                    Description = ex.Message,
                    Level = Level.记录,
                    Ip = ip,
                };
                logService.Save(log);
                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
