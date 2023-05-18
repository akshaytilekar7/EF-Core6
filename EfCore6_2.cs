/*

Tracking and Saving Data with EF Core 6

DbContext 
    -   Represents a session with the database
DbContext ChangeTracker
    -   Manages a collection of EntityEntry objects
EntityEntry
    -   State info for each entity: CurrentValues, OriginalValues, State enum, Entity & more
Entity
    -   are In-Memory Objects
    -   In-memory objects with key (identity) properties that the DbContext is aware of
    -   DbContext Contains EntityEntry objects with reference pointers to in memory objects

Tracking and Saving Workflow
    1.  EF Core creates tracking object for each entity
        - Tracking States can be : 
            Unchanged (Default) 
            Added 
            Modified 
            Deleted 
    2.  DbContext maintains EntityEntries like below
        -   {Julia Lerman,Modified}
        -   {Josie Newf, Added}
        -   {“How to Newf”, Added}
    3.  On SaveChanges
        -   use stateenum to decide insert, update, deleteS
        -   DbContext updates State for tracked entities
        -   Works with provider to compose SQL statements
        -   Executes statements on database
    4.  Returns #Affected & new PKs
    5.  Updates entity PKs and FKS
    6.  Resets state info (entity entry may state to unchange)

Add from DbSet or DbContext
    -   context.Authors.Add(…)
        -   Track via DbSet
        -   DbSet knows the type DbContext knows that it’s Added
    -   context.Add(…)
        -   Track via DbContext
        -   DbContext will discover type Knows it’s Added

    -   context.Authors.Add(author);
        context.SaveChanges(); 
        -   generate SQL insert statement and also one more query for fetching PK
        -   savechanges : assign PK new value to enitity
                        : reset its stateenum to unmodified, so calling SaveChanges() again wont add entity again and 
                          its just ignore add beacuse stateenum value is unchange 
Updating track entity 
    -    return number of row affected after query execution (@@RowCount)
    Example
        -   var empList = dbConext.Employee.Where(x => x.Name = "Akshay").ToList();
            empList.SaveChanges(); // if  empList has 3 object then it send 3 seaprate sql query and execute returns 3
        
    How DbContext Change tracker learn about changes? how they came to know about changes?
        -   Note    - context is not managing enities directly
                    -   calling savechanges() then 
                        context will take one last look at objects to determine their state and property values   
                        and this is done by DetectChanges()

    DetectChanges()
        -   read the values of entities its tracking and update the state details in tracking object being manage by ChangeTracker
        -   SaveChanges call DetectChanges() as part of process
        -   You can call DetectChanges() in your code if needed

    DbContext.ChangeTracker.DetectChanges()
        -   Reads each object being tracked and updates state details in EntityEntry object
    DbContext.SaveChanges()
        -   Always calls DetectChanges for you

    Example 
        -   var empList = dbConext.Employee.Where(x => x.Name = "Akshay").ToList();
            foreach(var item in empList) {item.Name = "newName"};

            C.W(dbContext.ChangeTracker.DebugView.ShortView);
            dbConext.Savechanges();
            C.W(dbContext.ChangeTracker.DebugView.ShortView);
        -   output
            Employee {Id : 1} Unchanged
            Employee {Id : 4} Unchanged
            Employee {Id : 144} Unchanged

            Employee {Id : 1} Modified
            Employee {Id : 4} Modified
            Employee {Id : 144} Modified
    

    The entities are just plain objects and don’t communicate their changes to the DbContext.
    entites dont have way to communicating back to dbContext when you make any change in entity
    but context have refrances to each of thoie enitity and DetectChanges() go and read values anf determine their state

Updating Untrack entity
    -   mostly in diconnected application like web app
    -   context.Employee.Update(obj)
        -   Track via DbContext
        -   DbContext will discover type Knows it’s Modified
    -   var emp = dbContext.Employyes.FirsOrDefault(x => x.Name == "Akshay");
        emp.Name = "Akshay New";
        dbContext.SaveChanges();

        geneated sql contain UPDATE tblEmployee SET Name = "Akshay New" where id = @id;
    

    -   var emp = dbContext.Employyes.FirsOrDefault(x => x.Name == "Akshay");
        emp.Name = "Akshay New";
        dbContext.Employee.Update(emp);

        geneated sql contain UPDATE tblEmployee SET Name = "Akshay New", Email = @email where id = @id; // include all proprties as no tracking
*/ 