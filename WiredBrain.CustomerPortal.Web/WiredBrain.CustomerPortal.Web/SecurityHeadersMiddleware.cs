using Microsoft.AspNetCore.Builder;

namespace WiredBrain.CustomerPortal.Web
{
    public static class SecurityHeadersMiddleware
    {
        public static void UseSecurityHeaders(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("HeaderType", "HeaderValue");
                await next();
            });
        }
    }
}
