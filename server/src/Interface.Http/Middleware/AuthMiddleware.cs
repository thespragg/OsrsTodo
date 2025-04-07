using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Interface.Http.Middleware;
    
internal static class AuthMiddleware
{
    internal static IServiceCollection AddAuthMiddleware(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var encryptionKey = Encoding.UTF8.GetBytes(configuration["Jwt:EncryptionKey"]!);

                var ecKey = new byte[256 / 8];
                Array.Copy(encryptionKey, ecKey, 256 / 8);
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    TokenDecryptionKey = 
                        new SymmetricSecurityKey(ecKey)
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => 
                policy.RequireRole("Admin"));
        });

        return services;
    }
}