using DropShot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropShot.Infrastructure.DAL.Configurations;

public class DropItemConfiguration : IEntityTypeConfiguration<DropItem>
{
    public void Configure(EntityTypeBuilder<DropItem> builder)
    {
    }
}