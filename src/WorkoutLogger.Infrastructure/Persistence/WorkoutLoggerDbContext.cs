using Microsoft.EntityFrameworkCore;

namespace WorkoutLogger.Infrastructure.Persistence;

public class WorkoutLoggerDbContext : DbContext
{
    public WorkoutLoggerDbContext(DbContextOptions options)
        : base(options)
    {
    }
}