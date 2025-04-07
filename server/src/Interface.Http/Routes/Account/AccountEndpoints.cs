using Interface.Http.Extensions;
using Interface.Http.Routes.Account.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Interface.Http.Routes.Account;

internal static class AccountEndpoints
{
    internal static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder routes)
        => routes.MapGroup("accounts", "Accounts", group =>
        {
            group.MapPost("login", LoginHandler.Handle).AllowAnonymous();
            group.MapPost("register", RegisterHandler.Handle).AllowAnonymous();
        });
}