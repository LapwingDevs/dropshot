using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;

namespace DropShot.Application.Products.Models;

public class VariantOnProductDto: IMapFrom<Variant>
{
    public int Id { get; set; }
    public int Size { get; set; }
}