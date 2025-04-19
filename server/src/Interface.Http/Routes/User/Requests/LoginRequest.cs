using JetBrains.Annotations;

namespace Interface.Http.Routes.User.Requests;

[UsedImplicitly]
internal sealed record LoginRequest(
    string Username,
    string Password
);