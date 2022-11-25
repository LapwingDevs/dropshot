using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;

namespace DropShot.Application.Drops.Models;

public class DropDetailsDto : IMapFrom<Drop>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public ICollection<DropItemDto> AvailableDropItems { get; set; }
    public ICollection<DropItemDto> ReservedDropItems { get; set; }
}