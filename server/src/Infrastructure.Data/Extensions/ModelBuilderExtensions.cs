using System.Linq.Expressions;
using Infrastructure.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Extensions;

internal static class ModelBuilderExtensions
{
    internal static void SetEnumStringConverter(
        this ModelBuilder modelBuilder
    )
    {
        var properties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType).IsEnum);

        foreach (var property in properties)
            property.SetProviderClrType(typeof(string));
    }

    internal static void ApplySoftDeleteQueryFilter(
        this ModelBuilder modelBuilder
    )
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                continue;

            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var property = Expression.Property(parameter, nameof(ISoftDelete.DeletedAt));
            var nullConstant = Expression.Constant(null, typeof(DateTime?));
            var condition = Expression.Equal(property, nullConstant);
            var lambda = Expression.Lambda(condition, parameter);

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }
    }
}