using Domain.Contracts;
using Domain.Models;
using Interface.Http.Extensions;
using Interface.Http.Routes.Account.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Http.Routes.Account.Handlers;

internal static class RegisterHandler
{
    internal static async Task<Results<Ok<AccessToken>, ProblemHttpResult>> Handle(
        RegisterRequest request,
        [FromServices] IAuthService authService,
        CancellationToken cancellationToken
    ) => await authService
        .RegisterAsync(request.Username, request.Email, request.Password, cancellationToken)
        .ToHttpResult();
}