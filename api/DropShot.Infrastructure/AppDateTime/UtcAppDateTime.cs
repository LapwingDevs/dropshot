using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;

namespace DropShot.Infrastructure.AppDateTime;

public class UtcAppDateTime : IAppDateTime
{
    public DateTime Now => DateTime.UtcNow;
}