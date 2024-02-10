## Issues while learning asp.net core API

## Issue 1
> This localhost page can’t be found no web page was found for the web address: https://localhost:7073/swagger
HTTP ERROR 404

**Reason:**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

**Solution:** 
Due to the condition we got that issue
    app.UseSwagger();
    app.UseSwaggerUI();

---

## Issue 2:
> Unable to create a 'DbContext' of type ''. The exception 'Unable to resolve service for type 'Microsoft.EntityFrameworkCore.DbContextOptions' while attempting to activate 'WalkMe.API.Data.WalkMeDbContext'.' was thrown while attempting to create an instance. For the different patterns supported at design time, see https://go.microsoft.com/fwlink/?linkid=851728

**Reason**
Wrong Configuration
```
public class WalkMeDbContext(DbContextOptions dbContextOptions)
    : DbContext(dbContextOptions)
{

}
```
**Solution:**
```
public class WalkMeDbContext(DbContextOptions<WalkMeDbContext> dbContextOptions)
    : DbContext(dbContextOptions)
{

}
```

---

## Issue 3
> Only the invariant culture is supported in globalization-invariant mode. See https://aka.ms/GlobalizationInvariantMode for more information. (Parameter 'name')
en-us is an invalid culture identifier.

**Reason**:
```<InvariantGlobalization>true</InvariantGlobalization>```

**Reference Article:** 
```https://learn.microsoft.com/en-us/dotnet/core/runtime-config/globalization?ranMID=46131&ranEAID=a1LgFw09t88&ranSiteID=a1LgFw09t88-TL.gIntYvAARbx7QNuWPjg&epi=a1LgFw09t88-TL.gIntYvAARbx7QNuWPjg&irgwc=1&OCID=AID2000142_aff_7806_1243925&tduid=(ir__mrdukhopi0kfqxeykk0sohzifv2xuvvxe2xvqk3y00)(7806)(1243925)(a1LgFw09t88-TL.gIntYvAARbx7QNuWPjg)()&irclickid=_mrdukhopi0kfqxeykk0sohzifv2xuvvxe2xvqk3y00#invariant-mode```

Only the invariant culture is supported in globalization-invariant mode" with the parameter 'name' being "en-us". This error typically occurs when an application is running in globalization-invariant mode, which restricts cultural formatting and localization to the "invariant" culture (neutral language, typically English).

1. **Application Configuration:**

> **Check application settings:** Look for any configuration settings related to globalization or culture. Ensure they're not explicitly set to "invariant" mode. If you find such settings, switch them to allow specific cultures or a default culture like "en-US".

> **Environment variables:** Verify if any environment variables related to globalization or culture are set, potentially overriding app settings.

2. **Framework-Specific Solutions:**

> .NET: If you're using .NET 8 or later, check the DOTNET_SYSTEM_GLOBALIZATION_INVARIANT environment variable. If it's set to true, set it to false to allow non-invariant cultures.

> Other frameworks: Consult documentation for your specific framework to see if there are specific ways to handle globalization-invariant mode and specify allowed cultures.


**Solution**:
```<InvariantGlobalization>false</InvariantGlobalization>```

---


## Issue 4:
 > System.InvalidOperationException: Multiple constructors accepting all given argument types have been found in type 'WalkMe.API.Controllers.RegionsController'. There should only be one applicable constructor.

 **Reason**:
 ```
 public class RegionsController : ControllerBase
 {
        private readonly WalkMeDbContext dbContext;

        public RegionsController(WalkMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
 }
 ```
 **Solution**:
 ```
 public class RegionsController(WalkMeDbContext dbContext) : ControllerBase
 {
        private readonly WalkMeDbContext dbContext = dbContext;
 }
 ```
**Note:** Here, the observation is in DBContext we changed construtor declaration as part of newer framework but in controller we did traditional approach.
Either you follow every where with same pattern or else you get this error.

---
## Issue 5:
> Exception:  Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HN19T07IH0F4", Request id "0HN19T07IH0F4:00000009": An unhandled exception was thrown by the application.
      System.InvalidOperationException: No route matches the supplied values.

**Reason:**
We added this line after update the records
```
[Route("{id:Guid}")]
```

**Soultion:**
```
[Route("{id:guid}")]
```

---
