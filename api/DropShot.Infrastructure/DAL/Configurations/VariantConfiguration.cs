using DropShot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropShot.Infrastructure.DAL.Configurations;

public class VariantConfiguration : IEntityTypeConfiguration<Variant>
{
    public void Configure(EntityTypeBuilder<Variant> builder)
    {
    }
}