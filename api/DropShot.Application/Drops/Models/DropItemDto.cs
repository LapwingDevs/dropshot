using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;

namespace DropShot.Application.Drops.Models;

public class DropItemDto : IMapFrom<DropItem>
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int VariantId { get; set; }
}