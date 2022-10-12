using LightGame.Common;
using LightGame.Protocol;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LightGame.API
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private ILogger _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new LGResponse { ErrorCode = (int)LGErrorType.Hidden, ErrorInfo = "未知错误" };
            context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
