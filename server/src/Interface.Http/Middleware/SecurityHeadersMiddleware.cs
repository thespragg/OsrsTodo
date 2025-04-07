using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Interface.Http.Middleware;

internal static class SecurityHeadersMiddleware
{
    internal static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Append("X-Frame-Options", "DENY");
            context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Append("Referrer-Policy", "no-referrer");
            context.Response.Headers.Append("Content-Security-Policy", 
                "default-src 'self'; frame-ancestors 'none'");
            await next();
        });
    }
}