using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DropShot.Infrastructure.DAL.Configurations;

public class DropItemConfiguration : IEntityTypeConfiguration<DropItem>
{
    public void Configure(EntityTypeBuilder<DropItem> builder)
    {
        builder.Property(v => v.Status)
            .HasDefaultValue(DropItemStatus.Available)
            .HasConversion(new EnumToStringConverter<DropItemStatus>());
    }
}