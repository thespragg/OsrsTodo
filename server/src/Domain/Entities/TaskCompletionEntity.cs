namespace Domain.Entities;

public sealed class TaskCompletionEntity
{
    public Guid Id { get; init; }
    public Guid TaskId { get; init; }
    public Guid UserId { get; init; }
    public DateTime CompletionDate { get; init; }
    public int TotalCompletions { get; init; }
}