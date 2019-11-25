using V.NetCore.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace V.NetCore.Infrastructure
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var type = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            Log4NetHelper.WriteError(type, context.Exception);
        }
    }
}
