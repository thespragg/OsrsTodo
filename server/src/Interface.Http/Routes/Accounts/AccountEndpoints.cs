using Interface.Http.Extensions;
using Interface.Http.Routes.Accounts.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Interface.Http.Routes.Accounts;

internal static class AccountEndpoints
{
    internal static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder routes)
        => routes.MapGroup("accounts", "Accounts", group =>
        {
            group.MapGet("", GetAccountsHandler.Handle).RequireAuthorization();
            group.MapPost("", CreateAccountHandler.Handle).RequireAuthorization();
        });
}