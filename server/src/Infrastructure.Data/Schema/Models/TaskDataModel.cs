using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.Contracts;

namespace Infrastructure.Data.Schema.Models;

internal sealed class TaskDataModel : ISoftDelete
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserDataModel? User { get; set; }
    [MaxLength(100)] public required string Name { get; set; }
    [MaxLength(200)] public string Description { get; set; } = string.Empty;
    public int InstancesPerDay { get; set; }
    public DateTime? DeletedAt { get; set; }
}