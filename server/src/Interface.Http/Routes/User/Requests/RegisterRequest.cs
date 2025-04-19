using JetBrains.Annotations;

namespace Interface.Http.Routes.User.Requests;

[UsedImplicitly]
internal sealed record RegisterRequest(
    string Username,
    string Email,
    string Password
);