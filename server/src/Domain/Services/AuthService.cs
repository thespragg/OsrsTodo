using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Functional.Sharp.Errors;
using Functional.Sharp.Models;
using Functional.Sharp.Monads;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Services;

internal sealed class AuthService(
    IUserRepository userRepository,
    IConfiguration configuration
) : IAuthService
{
    public async Task<Result<AccessToken>> RegisterAsync(
        string username,
        string email,
        string password,
        CancellationToken cancellationToken
    )
    {
        var userExists = await UserExistsAsync(username, email, cancellationToken);
        if (userExists) return new Error("A user with that email or username already exists.");

        var passwordErrMessage = ValidatePassword(password).Match(_ => string.Empty, err => err.Message);
        if (!string.IsNullOrEmpty(passwordErrMessage)) return new Error(passwordErrMessage);

        var (hash, salt) = CreatePasswordHash(password);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            Username = username,
            PasswordHash = hash,
            PasswordSalt = salt,
            FailedLoginAttempts = 0,
            LastLoginAt = DateTime.UtcNow,
            Role = Role.User
        };

        var result = await userRepository.Create(
            user,
            x => x.Id,
            cancellationToken
        );

        return result.Map(_ =>
        {
            var (jwt, expires) = GenerateJwtToken(user);
            return new AccessToken(
                jwt,
                expires
            );
        });
    }

    public async Task<Result<AccessToken>> LoginAsync(
        string username,
        string password,
        CancellationToken cancellationToken
    )
    {
        var userResult = await userRepository.FindOne(x => x.Username == username, cancellationToken);
        return userResult
            .Match<Result<AccessToken>>(user =>
                {
                    var passwordMatches = VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
                    if (!passwordMatches) return new Error("Incorrect username or password");

                    var (jwt, expires) = GenerateJwtToken(user);
                    return new AccessToken(
                        jwt,
                        expires
                    );
                },
                err => err is NotFoundError ? new Error("Incorrect username or password") : err
            );
    }

    private (string jwt, DateTime expiry) GenerateJwtToken(
        UserEntity user
    )
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        var encryptionKey = Encoding.UTF8.GetBytes(configuration["Jwt:EncryptionKey"]!);

        var ecKey = new byte[256 / 8];
        Array.Copy(encryptionKey, ecKey, 256 / 8);

        var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpireMinutes"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature),
            EncryptingCredentials = new EncryptingCredentials(
                new SymmetricSecurityKey(ecKey),
                SecurityAlgorithms.Aes256KW,
                SecurityAlgorithms.Aes256CbcHmacSha512),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), expires);
    }

    private static (string Hash, string Salt) CreatePasswordHash(
        string password
    )
    {
        using var hmac = new HMACSHA512();
        var salt = Convert.ToBase64String(hmac.Key);
        var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return (hash, salt);
    }

    private bool VerifyPasswordHash(
        string password,
        string storedHash,
        string storedSalt
    )
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        using var hmac = new HMACSHA512(saltBytes);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(Convert.FromBase64String(storedHash));
    }

    private Result<Unit> ValidatePassword(string password)
    {
        if (password.Length < 8)
            return new Error("Password must be at least 8 characters");

        if (!password.Any(char.IsUpper))
            return new Error("Password must contain uppercase letters");

        if (!password.Any(char.IsLower))
            return new Error("Password must contain lowercase letters");

        if (!password.Any(char.IsDigit))
            return new Error("Password must contain numbers");

        if (password.All(char.IsLetterOrDigit))
            return new Error("Password must contain special characters");

        return new Unit();
    }

    private async Task<bool> UserExistsAsync(
        string username,
        string email,
        CancellationToken cancellationToken
    ) => (await userRepository.FindOne(x => x.Username == username || x.Email == email, cancellationToken))
        .Match(
            _ => true,
            err => err is not NotFoundError
        );
}