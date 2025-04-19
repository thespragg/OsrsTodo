using Domain.Contracts.Repositories;
using Domain.Entities;
using Interface.Http.Extensions;
using Interface.Http.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Interface.Http.Routes.Accounts.Handlers;

internal static class GetAccountsHandler
{
    internal static async Task<Results<Ok<IEnumerable<AccountEntity>>, ProblemHttpResult>> Handle(
        [FromServices] ICurrentUserService currentUserService,
        [FromServices] IAccountRepo accountRepo,
        CancellationToken cancellationToken
    ) => await accountRepo
        .Find(x => x.UserId == currentUserService.UserId, cancellationToken)
        .MatchToHttp(TypedResults.Ok);
}