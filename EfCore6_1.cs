/*


Entity Framework Core
    -   Microsoft’s cross-platform data access framework for .NET
    -   Microsoft’s official data access technology for .NET development
    -   ~10 million of EF Core 6 in first 3 weeks
    -   Windows, macOS & Linux
    -   320 million downloads

    -   EF Core Began as an ORM (Object Relational Mapper)
    -   gives Developer productivity and Coding consistency
    -   EF cire differ from typical ORM, it has mapping layer in between
        Domain classes --- Mapping -- DB Schema
    -   Nuget
        EF Core SQL Server -> EF Core Relational -> RF Core -> .NET 6
    -   You must specify data provider and connection string

    -   DbContext + Conventional custom mappings => Database schema

EF Query workflow
    -   Express and execute query
    -   EF Core reads model, works with provider to work out SQL 
        -   internally EF core cache this query to avoid repetive work
    -   Sends SQL to database 
    -   Receives tabular results
    -   Materializes results as objects
    -   Adds tracking details to DbContext instance
        -   keeping track of object that is return by query

Database connection opens during query enumeration
    -   avoid, its a bad enumeration
            foreach(var item in dbContext.Authors) {}
    -   use this, performance improvement
            var list = dbContext.Authors
            foreach(var item in list) {}

    -   no sql injection becz EF uses paramerized query in some situation
        -   EF dont use parametrized query when we use value is directly in query
                .Where(a=>a.FirstName==“Josie”)   >>>  SELECT * FROM Authors WHERE Authors.FirstName=‘Josie’
        -   use value from variable 
                var name=“Josie”                  >>>  @P1=‘Josie’
                .Where(a=>a.FirstName==name)      >>>  SELECT * FROM Authors WHERE Authors.FirstName=@P1
    -   Skip and take transfer to offset and fetch next

-   OrderBy
    -   _context.Authors.OrderBy(x => x.FirstName)
                        .OrderBy(x => x.LastName) // for 2 orderby only last one consider
    -   _context.Authors.OrderBy(x => x.FirstName)
                         .ThenBy(x => x.LastName) // good and correct

-   enhancing Query Performance by when tracking isn't needed as CHANGE TRACKING IS EXPENSIVE

-   No tracking Query and No traking DbContext
    -   var author = context.Authors.AsNoTracking().FirstOrDefault();  //  AsNoTracking() returns a query, not a DbSet
        OR
    -   All queries following DbContext will default to no tracking
        -   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(myconnectionString)
                              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        -   Use DbSet.AsTracking() for SPECIAL QUERIES THAT NEED TO BE TRACKED

    

*/ 