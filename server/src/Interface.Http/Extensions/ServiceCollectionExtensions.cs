using Domain.Contracts;
using Interface.Http.Interfaces;
using Interface.Http.Middleware;
using Interface.Http.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Interface.Http.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpInterface(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services
        .AddHttpContextAccessor()
        .AddAuthMiddleware(configuration)
        .AddScoped<ICurrentUserService, CurrentUserService>();
}