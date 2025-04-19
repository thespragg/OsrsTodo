namespace Domain.Entities;

public sealed record AccountEntity
{
    public Guid Id { get; init; }
    public required string Username { get; init; }
    public Guid UserId { get; init; }
}