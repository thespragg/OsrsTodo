using Infrastructure.Data.Extensions;
using Infrastructure.Data.Interceptors;
using Infrastructure.Data.Schema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Data.Schema;

internal sealed class OsrsTodoDbContext(
    DbContextOptions options
) : DbContext(options)
{
    public DbSet<UserDataModel> Users { get; init; } = null!;
    public DbSet<AccountDataModel> Accounts { get; init; } = null!;
    public DbSet<TaskDataModel> Tasks { get; init; } = null!;
    public DbSet<TaskCompletionDataModel> TaskCompletions { get; init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .AddInterceptors(new SoftDeleteInterceptor());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SetEnumStringConverter();
        modelBuilder.ApplySoftDeleteQueryFilter();

        modelBuilder
            .Entity<UserDataModel>(user =>
            {
                user.HasKey(x => x.Id);
                user
                    .Property(x => x.Id)
                    .HasValueGenerator<GuidValueGenerator>();

                user
                    .HasMany(x => x.Accounts)
                    .WithOne()
                    .HasForeignKey(x => x.UserId);
            });

        modelBuilder.Entity<AccountDataModel>(account =>
        {
            account
                .Property(x => x.Id)
                .HasValueGenerator<GuidValueGenerator>();
            
            account.HasKey(x => x.Id);
        });
        
        modelBuilder
            .Entity<TaskDataModel>(task =>
            {
                task.HasKey(x => x.Id);
                task
                    .Property(x => x.Id)
                    .HasValueGenerator<GuidValueGenerator>();

                task
                    .HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
            });

        modelBuilder
            .Entity<TaskCompletionDataModel>(taskCompletion =>
            {
                taskCompletion.HasKey(x => x.Id);
                taskCompletion
                    .Property(x => x.Id)
                    .HasValueGenerator<GuidValueGenerator>();

                taskCompletion
                    .HasOne<TaskDataModel>()
                    .WithMany()
                    .HasForeignKey(x => x.TaskId);

                taskCompletion
                    .HasOne<UserDataModel>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
            });
    }
}