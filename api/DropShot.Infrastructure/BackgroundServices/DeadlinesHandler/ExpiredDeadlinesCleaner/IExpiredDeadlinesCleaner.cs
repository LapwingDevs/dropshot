using DropShot.Application.Common;

namespace DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.ExpiredDeadlinesCleaner;

public interface IExpiredDeadlinesCleaner
{
    Task Clean(IDbContext dbContext, CancellationToken cancellationToken);
}