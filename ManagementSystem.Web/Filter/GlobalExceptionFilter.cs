
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Model;

namespace ManagementSystem.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            //异常返回结果包装
            var rspResult = MessageModel<object>.FailResult(context.Exception.Message);
            //日志记录
            _logger.LogError(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;
            context.Result = new InternalServerErrorObjectResult(rspResult);
        }
        public class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(object value) : base(value)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
