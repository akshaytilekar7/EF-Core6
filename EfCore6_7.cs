/*

EF Core Transaction Basics
    -   SaveChanges is always wrapped in a DB Transaction
    -   Control workflow of default via Database.Transaction
    -   Override with an ADO.NET database transaction
    -   Override with System.Transactions

Example -   Cancel a Book Contract :-  Delete the book and Add artist notes about the cancellation
        -   see TransactionDemo.cs

what if connetion lost
    retry using 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(myconnection,options => options.EnableRetryOnFailure());
        }

override SaveChanges
    Example to update shadow peropty

    modelBuilder.Entity<Author>().Property<DateTime>("LastUpdated");

    private void UpdateAuditData()
    {
        foreach(var e in ChangeTracker.Entries().Where(e=>e.Entity is Author))
            entry.Property("LastUpdated").CurrentValue = DateTime.Now;
    }

    public override int SaveChanges()
    {
        UpdateAuditData();
        return base.SaveChanges();
    }
    // we can update CreateDate, UpdatedDate and CreatedBy for all enotites


Intersting
    -   Using EF Core Pipeline Events
        -   Events Exposed in EF Core API
            -   DbContext.SavingChanges
            -   DbContext.SavedChanges
            -   DbContext.SaveChangesFailed
            -   ChangeTracker.Tracked
            -   ChangeTracker.StateChanged

Implementing Event Handlers
    -   A method to execute custom logic
    -   Declare the method as a handler of the target event

code clener
    SaveChange Events vs. Override SaveChanges

Individual Event Handlers
        private void SavingChangesHandler(...)
        {
          //logic before save
        }

        private void SavedChangesHandler(...)
        {
         //logic after save
        }

        private void SaveFailedHandler(...)
        {
         //error handling
        }

Messay
    Override SaveChanges
    public override int SaveChanges()
    {
        try
        {
            //some actions before save
            int affected= base.SaveChanges();
            //some actions after saved
            return affected;
        }
        catch
        {
            //save failed: error handling
        }
    }


-   Using Interceptors to Inject Logic into EF Core’s Pipeline
    -   Entity Framework Core (EF Core) interceptors enable interception,
        modification, and/or suppression of EF Core operations. This includes
        low-level database operations such as executing a command, as well as
        higher-level operations, such as calls to SaveChanges


Interceptor
    -   IDbCommandInterceptor
            Before sending a command to the database
            After the command has executed
            Command failures
            Disposing the command's DbDataReader
    -   IDbConnectionInterceptor
            Opening and closing connections
            Connection failures
    -   IDbTransactionInterceptor
            Creating transactions
            Using existing transactions
            Committing transactions
            Rolling back transactions
            Creating and using savepoints
            Transaction failures


Setting Up Interception
    // Define an interceptor class
    internal class MyInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> 
        ReaderExecuting(DbCommand command,CommandEventData eventData,InterceptionResult<DbDataReader> result)
        {
            //do something e.g., edit command
            return result;
        }
    }

    // Register the interceptor in your DbContext
    optionsBuilder.UseSqlServer(connString).AddInterceptors(new MyInterceptor());

    // Or via ASP.NET Core services configuration
    builder.Services.AddDbContext(options=> options.AddInterceptors(new MyInterceptor());
 */ 