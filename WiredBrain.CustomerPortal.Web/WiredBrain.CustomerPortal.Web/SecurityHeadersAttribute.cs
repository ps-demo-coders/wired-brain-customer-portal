using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WiredBrain.CustomerPortal.Web
{
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;

            if (result is ViewResult)
            {
                context.HttpContext.Response.Headers
                    .Add("content-security-policy", "default-src 'self'; style-src https://stackpath.bootstrapcdn.com;");
            }
        }
    }
}
