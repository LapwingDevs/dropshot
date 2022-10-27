using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;

namespace DropShot.Application.Drops.Models;

public class CreateDropItemDto : IMapFrom<DropItem>
{
    public int Quantity { get; set; }
    public int VariantId { get; set; }
}