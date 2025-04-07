using Domain.Contracts;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
        => services.AddTransient<IAuthService, AuthService>();
}