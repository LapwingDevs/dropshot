using DropShot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropShot.Infrastructure.DAL.Configurations;

public class DropConfiguration : IEntityTypeConfiguration<Drop>
{
    public void Configure(EntityTypeBuilder<Drop> builder)
    {
    }
}