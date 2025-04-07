using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Extensions;

internal static class ModelBuilderExtensions
{
    internal static void SetEnumStringConverter(this ModelBuilder modelBuilder)
    {
        var properties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType).IsEnum);

        foreach (var property in properties)
            property.SetProviderClrType(typeof(string));
    }

}