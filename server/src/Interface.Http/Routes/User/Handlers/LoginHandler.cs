using Domain.Contracts;
using Domain.Models;
using Interface.Http.Extensions;
using Interface.Http.Routes.User.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Http.Routes.User.Handlers;

internal static class LoginHandler
{
    internal static async Task<Results<Ok<AccessToken>, ProblemHttpResult>> Handle(
        LoginRequest request,
        [FromServices] IAuthService authService,
        CancellationToken cancellationToken
    ) => await authService
        .LoginAsync(request.Username, request.Password, cancellationToken)
        .ToHttpResult();
}