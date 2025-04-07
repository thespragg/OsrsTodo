namespace Interface.Http.Routes.Account.Requests;

internal sealed record LoginRequest(
    string Username,
    string Password
);