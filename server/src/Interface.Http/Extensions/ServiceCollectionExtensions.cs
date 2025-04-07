using Interface.Http.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Interface.Http.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpInterface(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services.AddAuthMiddleware(configuration);
}