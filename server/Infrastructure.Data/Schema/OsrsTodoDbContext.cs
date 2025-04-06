using Infrastructure.Data.Extensions;
using Infrastructure.Data.Schema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Data.Schema;

internal sealed class OsrsTodoDbContext(
    DbContextOptions options
) : DbContext(options)
{
    public DbSet<UserDataModel> Users { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SetEnumStringConverter();

        modelBuilder
            .Entity<UserDataModel>(user =>
            {
                user
                    .Property(x => x.Id)
                    .HasValueGenerator<GuidValueGenerator>();
            });
    }
}