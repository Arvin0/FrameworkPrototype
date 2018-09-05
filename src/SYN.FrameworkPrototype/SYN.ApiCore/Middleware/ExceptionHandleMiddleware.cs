using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SYN.ApiCore.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandleMiddleware> _logger;

        public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, context.Response.StatusCode, e.ToString());
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = string.Empty;
                switch (statusCode)
                {
                    case 401:
                        msg = "未授权";
                        break;
                    case 404:
                        msg = "未找到服务";
                        break;
                    case 502:
                        msg = "请求错误";
                        break;
                    default: break;
                }

                if (statusCode != 200)
                {
                    msg = "未知错误";
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    await HandleExceptionAsync(context, statusCode, msg);
                }
            }
        }

        private Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new {code = statusCode.ToString(), is_success = false, msg = msg};
            var result = JsonConvert.SerializeObject(new {data = data});
            context.Response.ContentType = "application/json;charset=utf-8";

            try
            {
                _logger.LogWarning(msg);
            }
            catch (Exception)
            {
                // Catch日志操作异常，以便于不影响主逻辑
            }

            return context.Response.WriteAsync(result);
        }
    }
}
