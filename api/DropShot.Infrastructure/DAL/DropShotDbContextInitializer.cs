using DropShot.Application.Common;

namespace DropShot.Infrastructure.DAL;

public class DropShotDbContextInitializer
{
    private readonly IDbContext _dbContext;

    public DropShotDbContextInitializer(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
}