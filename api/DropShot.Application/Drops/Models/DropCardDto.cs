using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;

namespace DropShot.Application.Drops.Models;

public class DropCardDto : IMapFrom<Drop>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}