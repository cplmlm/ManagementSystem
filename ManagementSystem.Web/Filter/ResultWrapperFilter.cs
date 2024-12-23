using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Model;

namespace ManagementSystem.Filter
{
    public class ResultWrapperFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var actionWrapper = controllerActionDescriptor?.MethodInfo.GetCustomAttributes(typeof(NoWrapperAttribute), false).FirstOrDefault();
            var controllerWrapper = controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttributes(typeof(NoWrapperAttribute), false).FirstOrDefault();
            //如果包含NoWrapperAttribute则说明不需要对返回结果进行包装，直接返回原始值
            if (actionWrapper != null || controllerWrapper != null)
            {
                return;
            }
            //根据实际需求进行具体实现
            var rspResult = new MessageModel<object>();
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult?.Value == null)
                {
                    rspResult.Status = ResultStatus.Fail;
                    rspResult.Message = "未找到资源";
                    context.Result = new ObjectResult(rspResult);
                }
                else
                {
                   // 如果返回结果已经是ResponseResult<T> 类型的则不需要进行再次包装了//objectResult.DeclaredType.IsGenericType && 
                    if (objectResult.DeclaredType?.GetGenericTypeDefinition() == typeof(MessageModel<>))
                    {
                        return;
                    }
                    rspResult.Data = objectResult.Value;
                    rspResult.Status = ResultStatus.Fail;
                    context.Result = new ObjectResult(rspResult);
                }
                return;
            }
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NoWrapperAttribute : Attribute
    {
    }
}
