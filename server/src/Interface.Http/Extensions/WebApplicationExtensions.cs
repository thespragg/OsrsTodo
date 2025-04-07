using Interface.Http.Middleware;
using Interface.Http.Routes.Account;
using Microsoft.AspNetCore.Builder;

namespace Interface.Http.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseHttpInterface(
        this WebApplication webApplication
    )
    {
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
        
        webApplication.MapGet("status", () => true).RequireAuthorization();
        webApplication.UseSecurityHeaders();
        webApplication.MapAccountEndpoints();
        
        return webApplication;
    }
}