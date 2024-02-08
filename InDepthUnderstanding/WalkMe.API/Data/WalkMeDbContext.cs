using Microsoft.EntityFrameworkCore;

namespace WalkMe.API.Data;
public class WalkMeDbContext(DbContextOptions dbContextOptions)
    : DbContext(dbContextOptions)
{
    public DbSet<Difficulty> Difficulties { get; set; }

    public DbSet<Region> Regions { get; set; }

    public DbSet<Walk> Walks { get; set; }
}
