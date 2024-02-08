## Important information when we are working with .net core api

### For EF Core we require only two namespaces
1. Microsoft.EntityFrameworkCore.SqlServer
   - It contains Microsoft.EntityFramework
2. Microsoft.EntityFrameworkCore.Tools
   - It contains Microsoft.EntityFrameworkCore.Design

### Database Context
**DBContextOptions** we use for accessing configuration values from appsettings.json through Constructor Injection.
We pass this object to :base(dbContextOptions) like below

**Sample Code:**
```
        public WalkMeDbContext(DbContextOptions dbContextOptions) 
        : base(dbContextOptions)
        {
            // Meaning of this statement: public WalkMeDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
            // Passing current class dbContextOptions to base class
        }
```

### Terminologies
 - For any Database object Class == Domain/Entities
 - 

