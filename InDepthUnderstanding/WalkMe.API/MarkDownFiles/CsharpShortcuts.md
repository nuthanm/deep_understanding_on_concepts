## Rewrite Traditional way to simplified way

### I want to map list of domain data to dto

**Traditional Constructor:**
```
public class WalkMeDbContext : DbContext
{
        public WalkMeDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            // Meaning of this statement: public WalkMeDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
            // Passing current class dbContextOptions to base class
        }
}

```
**.NET 8 - Primary Constructor:**
```

        public class WalkMeDbContext(DbContextOptions dbContextOptions)
        : DbContext(dbContextOptions)
        {
            
        }
```
