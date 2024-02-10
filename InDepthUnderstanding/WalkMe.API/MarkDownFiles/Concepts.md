## Important information when we are working with .net core api

### For EF Core we require only two namespaces
1. Microsoft.EntityFrameworkCore.SqlServer
   - It contains Microsoft.EntityFramework
2. Microsoft.EntityFrameworkCore.Tools
   - It contains Microsoft.EntityFrameworkCore.Design

---

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

---

### Terminologies
 - For any Database object in Csharp : Class == Domain/Entities
 - 

---

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

---

### DTO's ( Data Transfer Objects) vs Domain Models
- ```DTO``` stands "Data Transfer Objects"
- In the name itself these objects are used for transfer between layers or over networks.
- Network(s) means client-server communication
- These are subset of domain models/Entity models
- ```Reason``` behind introduced these DTO's are 
  - No direct exposure of Domain models to client
  - With DTO's we can manipulate and send it back to client.
  - 

**Pros**
- Separation of Concerns: Domain models are tightly coupled with Database schema objects where as DTO's are not like that we use it for business purpose.
                           Advantage here is we can customize and manipulate on DTO's and no need of expoing all the columns to the client.
- Performance: No need of send all the columns from Domain model so there should be performance difference between DTO vs Domain model
- Security: Avoiding exposing all the data over the network to the client.
- Versioning

---

## Repository Pattern
- We use this to separate the data access layer from the application.
- Provides interface without exposing implementation
- It contains all the CURD operations and we give the method names from the controller
- This repository layer is in the middle between the Controller and the Database

**Diagram:**
![image](https://github.com/nuthanm/deep_understanding_on_concepts/assets/29816449/232134ec-54db-4220-9a05-94377e925f8d)

**Benefits:**
1. Decoupling
1. Consistency
1. Performance
1. Switching multiple data sources

---

## Automapper
- Useful to map object to object
- It means DTO to Domain or vice versa
- Simplify the process of mapping
- Avoid repeatable code and make it clean and simple
- PreRequisites:
  - Install ```Nuget Package```: Automapper
  - Create mapping profile class
    - Extend this class with ```Profile```
    - Create a constructor and create mapping
    - If properties are not match with entities like DTO and Domains then automapper won't work directly.
      - We set explicity map for this scenario
    - Inject this in startup.cs ```builder.Services.AddAutoMapper(typeof(AutoMappingProfile));```

**Example:** 
Class A contains FullName
Class B contains Name

> CreateMap<Source, Destination>
  .ForMember(x=>x.Name, opt => opt.MapFrom(x=>x.FullName)
  .ReverseMap()

**Example:** With out  ```Automapper```

**Domain models to DTO:**
```
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain is null)
            {
                return NotFound();
            }
            
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

```

**DTO to Domain models:**

```
            
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

```

**Example:** With ```Automapper```

**Domain models to DTO:**
```
             

```

**DTO to Domain models:**

```
            
           

```
---


## Clear on the following concepts
- HTTP Request Pipeline
- Importance of Middlewares and its usage in terms of overriding the existing implementation
- DTO's ( Data Transfer Objects) vs Domain Models
- Difference between AddSingleTon, AddScoped, AddTransient, AddTryScoped, AddTryTransient
- Difference between IActionResult vs ActionResult
- How to handle exceptions - At controllers/Services/Repository
- Order of Attributes: [Http] and then [Route("")] is the default one 
