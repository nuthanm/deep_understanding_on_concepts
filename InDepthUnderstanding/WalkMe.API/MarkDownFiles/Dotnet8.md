## Important information For .net 8

### Change traditional construct definition to primary constructor

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
