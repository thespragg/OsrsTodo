using Domain.Entities;
using Domain.Models;
using Functional.Sharp.Models;
using Functional.Sharp.Monads;

namespace Domain.Contracts;

public interface IAuthService
{
    Task<Result<AccessToken>> RegisterAsync(
        string username,
        string email,
        string password,
        CancellationToken cancellationToken
    );

    Task<Result<AccessToken>> LoginAsync(
        string username,
        string password,
        CancellationToken cancellationToken
    );
}