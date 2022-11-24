using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;

namespace DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.ExpiredDeadlinesCleaner;

public interface IExpiredDeadlinesCleaner
{
    Task Clean(IDbContext dbContext, CancellationToken cancellationToken);
}