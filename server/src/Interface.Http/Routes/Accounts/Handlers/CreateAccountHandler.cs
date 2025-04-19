using Domain.Contracts.Repositories;
using Domain.Entities;
using Interface.Http.Extensions;
using Interface.Http.Interfaces;
using Interface.Http.Routes.Accounts.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Http.Routes.Accounts.Handlers;

internal static class CreateAccountHandler
{
    internal static async Task<Results<Created<Guid>, ProblemHttpResult>> Handle(
        CreateAccountRequest request,
        [FromServices] IAccountRepo accountRepo,
        [FromServices] ICurrentUserService userService,
        CancellationToken cancellationToken
    ) => (await accountRepo.Create(new AccountEntity
        {
            Username = request.Username,
            UserId = userService.UserId!.Value
        },
        x => x.Id,
        cancellationToken)).MatchToHttp(x => TypedResults.Created("", x));
}