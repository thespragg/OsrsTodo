namespace Interface.Http.Routes.Account.Requests;

internal sealed record RegisterRequest(
    string Username,
    string Password
);