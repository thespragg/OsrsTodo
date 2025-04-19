using Interface.Http.Middleware;
using Interface.Http.Routes.Accounts;
using Interface.Http.Routes.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

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
        
        webApplication.MapUserEndpoints();
        webApplication.MapAccountEndpoints();

        if (!webApplication.Environment.IsDevelopment())
            return webApplication;

        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();

        return webApplication;
    }
}