using Microsoft.AspNetCore.Routing;

namespace Interface.Http.Extensions;

public static class ServiceCollectionExtensions
{
    public static IEndpointRouteBuilder AddEndpoints(
        this IEndpointRouteBuilder endpointsBuilder
    ) => endpointsBuilder
        .MapGroup("accounts", "Accounts", group => { });
}