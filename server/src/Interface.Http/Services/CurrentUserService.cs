using System.Security.Claims;
using Interface.Http.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Interface.Http.Services;

internal sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid? UserId
        => Guid.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var user)
            ? user
            : null;
}