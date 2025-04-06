using Domain.Enums;

namespace Domain.Entities;

public class UserEntity
{
    public Guid Id { get; init; }
    public required string Username { get; init; }
    public required string PasswordHash { get; init; }
    public required string PasswordSalt { get; init; }
    public Role Role { get; init; } = Role.User;
    public DateTime? LastLoginAt { get; init; }
    public int FailedLoginAttempts { get; init; } = 0;
}