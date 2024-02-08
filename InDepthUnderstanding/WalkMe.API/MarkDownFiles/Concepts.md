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
 - For any Database object in Csharp : Class == Domain/Entities
 - 


### DI - Dependency Injection

**Without DI**

```
public class EmployeeController: ControllerBase
{
    private readonly EmployeeService _empService;

    public EmployeeController()
    {
       _empService = new();
    }
}
```
**Disadvantage of without DI:**
1. Initially i used EmployeeService and lateron if any new version i want to create and inject in all the controllers
   then it's a difficult task.
   Ex: If i injected already in 10 controllers then i should go and change to EmpNewService in all controllers.

**With DI**

```
In Program.cs or Starup.cs

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

In Controllers/,
public class EmployeeController: ControllerBase
{
    private readonly IEmployeeService _empService;

    public EmployeeController(IEmployeeService empService)
    {
       _empService = empService;
    }
}
```
The above issue is easily solved. This means implementation of service is easily added and not effected in any where.
```
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IEmployeeService, EmpNewService>();
```

## Clear on the following concepts
- Http Request Pipeline
- Importance of Middlewares and it's usage in terms of override the existing implementation
- DTO's ( Data Transfer Objects) vs Domain Models
- Difference between AddSingleTon, AddScoped, AddTransient, AddTryScoped, AddTryTransient
- Differnece between IActionResult vs ActionResult
- How to handle exceptions - At controllers/Services/Repository