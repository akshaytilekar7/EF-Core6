/*
 
EF Core Allows You To Work Directly With
    -   Raw SQL 
    -   Stored Procedures 
    -   Views
    -   Table Value Functions
    -   Map to Queries in DbContext 
    -   Scalar Functions

Use Query Commands or Call Stored Procedures

    -   SELECT * FROM Authors
        -   Pass in the SQL

    -   EXEC MyStoredProc, p1, p1
        -   Execute stored procedures and pass in any needed parameters

    -   Two DbSet Methods to Query Using Your SQL
        -   DbSet Methods to Run Raw SQL

        -   DbSet.FromSqlRaw( )
            -   _context.Authors.FromSqlRaw(“some sql string”).ToList(); // Must Align with Entity Scalars & DB Columns

        -   DbSet.FromSqlInterpolated( )
            -   _context.Authors.FromSqlInterpolated($“some sql string {var}”).ToList();
        
        Retrieve entities without relying on LINQ or EF Core’s generated SQL
        Use parameters to avoid SQL injection
        Expects results to be in the shape of the DbSet’s entity
        Creates an IQueryable, so you still need an execution method

        
        -    LINQ methods such as Where and OrderBy
        -    LINQ execution methods like ToList and FirstOrDefault
        -    Iqueryable methods like Include and AsNoTracking
        -    Not DbSet methods e.g., Find!
        -   .FromSqlRaw("select * from authors").FirstOrDefault(a=>a.Id==3)
        -   .FromSqlRaw("select * from authors").OrderBy(a=>a.LastName)
        -   .FromSqlRaw("select * from authors").Include(a=>a.Books)

        -   limitation of raw sql
            -   Must return data for all properties of the entity type
            -   Column names in results match mapped column names
            -   Query can’t contain related data
            -   Only query entities and keyless entities known by DbContext

Interpolation for Parameters
    -   Interpolated Strings
        string name=“Josie”;
        string s=$“Happy Birthday, {name}”;

    Interpolated Strings in Raw SQL
        var lastnameStart = "L";
        var ontheflySQL = _context.Authors.FromSqlInterpolated(
                            $"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
                            .ToList();

        var storedProc = _context.Authors.FromSqlInterpolated(
                            $“EXEC FindAuthorByLastNameStart'{lastnameStart}%'")
                            .ToList();

Keeping Your Database Safe with Parameterized Raw SQL Queries
    -   Never use SQL with parameters embedded directly into the string
    

Combining Strings in C#
    -   Concatenated string
        -   string name=“Josie”;
            string s=“Happy Birthday,” + name;
    -   Formatted string
        -   string name=“Josie”;
            int age=5;
            string s= String.Format(“Happy {0} Birthday {1},”,age, name);
    -   Interpolated string
            string name=“Josie”;
            string s=$“Happy Birthday, {name}”;

    Note
        -   FromSqlInterpolated expects one formatted string as its parameter
        -   FromSqlInterpolated will not accept a string

    Safe Query strings
        -   Formatted as param of FromSqlRaw
        -   Interpolated as param of FromSqlInterpolated

    Unsafe Raw Queries
        -   All concatenated queries are unsafe
        -   Formatted strings in a variable
        -   Interpolated strings in a variable
        -   Won’t even compile:
                String in FromRawInterpolated
                Formatted string in FromSqlInterpolated


EF Core’s raw SQL methods support calling stored procedures
    -   EXEC thesproc startyear, endyear

    -   FromSqlRaw with formatted string
            DbSet.FromSqlRaw(“Exec MyStoredProc, {0}, {1}”, firstValue, secondValue)
    -   FromSqlInterpolated with interpolated string
            DbSet.FromSqlInterpolated($”EXEC MyStoredProc {firstValue}, {secondValue})

Querying via DbSets Using Stored Procedures
        Include values of expected parameters
        EF Core will transform this as needed by the database

VVIMP InMemory provider
    -   update the key property as soon as it ware of the enitity
        -   means before saveChange sits update PK

    
    -   if exist - <Nullable>enable</Nullable>
        public string Name {get;set;}
            -   gives warnign of nullable
            -   and it impamct mapping of DB means Name Column is DB is NotNull beacuse of this

    -   if we comment from DbContetx
        // public DbSet<Book> Books {get;set;}
        but still we can access books because its exist in Authors DbSet
        context will find books and included in data model
    -   Enitity<Author>().AutoInclude(a => a.Books); // always include when querying 

    DataAnnotation for Mapping
    -   only provide small subset of mapping
    -   should be avoided beacuse DOAMIN CLASSES SHOULD NOT KNOW ABOUT PERSISTANCE
    -   Mapping should not scatterd across classes it should be in DbContext only
    -   Annotations are applied at runtime and ignored by compiler 

    Side Note
        -   OnModelCreating  and OnConfiguring and ConfigureConvention
            -   these virtual method when we override it has called to base method aslo like base.MethidName()
            -   but this call is unnecessary becz these mthod are "No-Op" ie bosy is empty , No Operation
*/  