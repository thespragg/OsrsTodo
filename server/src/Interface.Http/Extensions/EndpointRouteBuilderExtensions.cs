using System.Diagnostics.CodeAnalysis;
using Interface.Http.Routes.Account;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Interface.Http.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    internal static IEndpointRouteBuilder MapGroup(
        this IEndpointRouteBuilder builder,
        [StringSyntax("Route")] string prefix,
        string groupName,
        Action<RouteGroupBuilder> configure)
    {
        var group = builder.MapGroup(prefix)
            .WithOpenApi()
            .WithTags(groupName)
            .RequireAuthorization();

        configure(group);

        return builder;
    }
}