namespace Domain.Entities;

public sealed record TaskEntity
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public UserEntity? User { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public int InstancesPerDay { get; init; }
}