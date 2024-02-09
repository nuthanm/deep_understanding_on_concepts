## This place is for you to have all the LINQ queries and its SQL queries

### Query 1: To get entire data from a table
#### Pattern 1: Without List() or ListAsync()
```
var regions = dbContext.Regions;
```

```
SQL Query: 
SELECT [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl] FROM[Regions] AS[r]
```
Return type for Pattern 1: ```DbSet<Region>```

#### Pattern 1: With out List() or ListAsync()
```
var regions = dbContext.Regions.ToList();
```

```
SQL Query: 
SQL Query: SELECT [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl] FROM[Regions] AS[r]
```
Return type for Pattern 2: ```List<Region>```

**Note:** No change in the query either way you can use it.
           
### Query 2: To get single data we have multiple options
#### Option 1: If you have a primary key column then use ```Find or FindAsync``` methods

**LinqQuery:** 
```
var region = dbContext.Regions.Find(id);
```
**SQL Query:**
```
SELECT TOP(1) [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl]
      FROM [Regions] AS [r]
      WHERE [r].[Id] = @__get_Item_0
```
**Points:**
- Query returns Nullable class ```Ex: Region?```
- This option is concise and straightforward. It's often used when you know you're querying by primary key (ID) and you expect the record to exist. 
- It's a bit ```more efficient than using a LINQ query because it directly fetches the entity``` by its primary key from the context if it's already loaded.

**Note:** Performance point of view ``` Find()``` is the best one only if you have a primary key.

#### Option 2: You can use even with the Where Clause with the same primary key but with this clause, you can use it with any other columns

**LinqQuery:** 
```
var region = dbContext.Regions.Where(x => x.Id == id).FirstOrDefault();
```
**SQL Query:**
```
 SELECT TOP(1) [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl]
      FROM [Regions] AS [r]
      WHERE [r].[Id] = @__id_0
```
**Points:**
- Query returns Nullable class ```Ex: Region?```
- This option is more flexible as it allows you to ```add additional conditions to the query if needed```. 
- It's useful ``` when you want to include more complex filtering logic beyond just the ID```. However, it's slightly ``` less efficient than Find``` because it involves translating the LINQ query to SQL and executing it against the database.

#### Option 3: You can even with FirstOrDefault you can achieve the same and it's a simplified way

**LinqQuery:** 
```
var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
```
**SQL Query:**
```
SELECT TOP(1) [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl]
      FROM [Regions] AS [r]
      WHERE [r].[Id] = @__id_0
```
**Points:**
- Query returns Nullable class ```Ex: Region?```
- This option combines the filtering and retrieval into a single LINQ statement. 
- It's ```similar to Option 2``` but uses a ```lambda expression``` directly within the FirstOrDefault method. 
- Like Option 2, it's flexible and ```suitable for more complex filtering requirements```.

#### Option 4: You can even with SingleOrDefault you can achieve the same and it's a simplified way

**LinqQuery:** 
```
var region = dbContext.Regions.SingleOrDefault(x => x.Id == id);
```
**SQL Query:**
```
SELECT TOP(2) [r].[Id], [r].[Code], [r].[Name], [r].[RegionImageUrl]
      FROM [Regions] AS [r]
      WHERE [r].[Id] = @__id_0
```
**Points:**
- Query returns Nullable class ```Ex: Region?```
- Instead of Top(1) in this query Top(2) - only this change in SQL query
- This option combines the filtering and retrieval into a single LINQ statement. 
- if you expect only one record to match the criteria, 
- and you want an ``` exception to be thrown if more than one record is found```

**Note:** No change in SQL Queries when you use any one of these options: Option 1, Option 2 and Option 3 
