using DropShot.Application.Common;

namespace DropShot.Infrastructure.AppDateTime;

public class UtcAppDateTime : IAppDateTime
{
    public DateTime Now => DateTime.UtcNow;
}