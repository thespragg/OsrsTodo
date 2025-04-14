namespace Infrastructure.Data.Schema.Models;

internal sealed class TaskCompletionDataModel
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CompletionDate { get; set; }
    public int TotalCompletions { get; set; }
}