using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Infrastructure.Data.Schema.Models;

internal sealed class UserDataModel
{
    public Guid Id { get; init; }
    [MaxLength(50)] public required string Username { get; init; }
    [MaxLength(50)] public required string Email { get; init; }
    [MaxLength(255)] public required string PasswordHash { get; init; }
    [MaxLength(255)] public required string PasswordSalt { get; init; }
    public Role Role { get; init; } = Role.User;
    public DateTime? LastLoginAt { get; init; }
    public int FailedLoginAttempts { get; init; }
}