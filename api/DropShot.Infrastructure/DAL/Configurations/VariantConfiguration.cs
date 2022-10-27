using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DropShot.Infrastructure.DAL.Configurations;

public class VariantConfiguration : IEntityTypeConfiguration<Variant>
{
    public void Configure(EntityTypeBuilder<Variant> builder)
    {
        builder.Property(v => v.Status)
            .HasDefaultValue(VariantStatus.Warehouse)
            .HasConversion(new EnumToStringConverter<VariantStatus>());
    }
}