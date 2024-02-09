## This place for you to have all the linq queries and its sql query

### Query 1: To get entire data from a table
#### Pattern 1: With out List() or ListAsync()
var regions = dbContext.Regions;
```
SQL Query: 
SELECT [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl] FROM[Regions] AS[r]
```
Return type for Pattern 1: ```DbSet<Region>```

#### Pattern 1: With out List() or ListAsync()
var regions = dbContext.Regions.ToList();
```
SQL Query: 
SQL Query: SELECT [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl] FROM[Regions] AS[r]
```
Return type for Pattern 2: ```List<Region>```

**Note:** No change in the query either way you can use it.
            
