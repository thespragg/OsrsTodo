namespace Interface.Http.Routes.Accounts.Requests;

internal sealed record CreateAccountRequest
{
    public required string Username { get; init; }
}