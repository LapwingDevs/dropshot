using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;

namespace DropShot.Application.Drops.Models;

public class DropItemDto
{
    public int DropItemId { get; set; }
    public int VariantId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public ClothesUnitOfMeasure UnitOfSize { get; set; }
    public int Size { get; set; }
    public DateTime? ReservationEndDateTime { get; set; }
}