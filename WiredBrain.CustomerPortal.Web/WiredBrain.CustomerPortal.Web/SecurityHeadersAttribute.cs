using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WiredBrain.CustomerPortal.AspNetCore
{
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;

            if (result is ViewResult)
            {
                context.HttpContext.Response.Headers
                    .Add("HeaderType", "HeaderValue");
            }
        }
    }
}
