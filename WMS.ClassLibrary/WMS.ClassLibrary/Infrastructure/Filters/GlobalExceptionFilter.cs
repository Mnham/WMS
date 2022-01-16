using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WMS.ClassLibrary.Infrastructure.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var resultObject = new
            {
                ExceptionType = context.Exception.GetType().FullName,
                context.Exception.Message,
                context.Exception.StackTrace
            };

            context.Result = new JsonResult(resultObject)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}