using Domain.Entities;

namespace Domain.Models;

public sealed record AccessToken(
    string Token,
    DateTime Expires
);