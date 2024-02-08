using Microsoft.EntityFrameworkCore;
using ProgramEntryPoint.API.Models.Domain_Models;

namespace ProgramEntryPoint.API.Data
{
    public class WalkMeDbContext : DbContext
    {
        public WalkMeDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            // Meaning of this statement: public WalkMeDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
            // Passing current class dbContextOptions to base class
        }


        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }
    }
}
