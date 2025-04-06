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
            );

        return services;
    }
}