using Domain.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataInfrastructure(
        this IServiceCollection services,
        string connString
    )
    {
        services
            .AddHealthChecks()
            .AddNpgSql(connString, name: "Database");

        services
            .AddDbContext<OsrsTodoDbContext>(
                (sp, options) =>
                    options
                        .UseNpgsql(
                            connString,
                            opts => opts.EnableRetryOnFailure(
                                5,
                                TimeSpan.FromSeconds(10),
                                null
                            )
                        )
                        .UseSnakeCaseNamingConvention()
            );

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAccountRepo, AccountRepo>();
        services.AddTransient<ITaskRepository, TaskRepository>();
        services.AddTransient<ITaskCompletionRepository, TaskCompletionRepository>();

        return services;
    }
}