using Interface.Http.Extensions;
using Interface.Http.Routes.User.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Interface.Http.Routes.User;

internal static class UserEndpoints
{
    internal static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder routes)
        => routes.MapGroup("user", "User", group =>
        {
            group.MapPost("login", LoginHandler.Handle).AllowAnonymous();
            group.MapPost("register", RegisterHandler.Handle).AllowAnonymous();
        });
}