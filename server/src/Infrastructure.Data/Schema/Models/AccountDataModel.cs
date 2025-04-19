using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Schema.Models;

internal sealed class AccountDataModel
{
    public Guid Id { get; init; }
    [MaxLength(50)] public required string Username { get; init; }
    public required Guid UserId { get; init; }
}