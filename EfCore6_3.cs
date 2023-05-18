/*

    All Insert Update Delete in EF are parameterized

-   ParentChildInsert.cs
    -   Add New Parent and New Child Together ways see 
    -   Add a New Child to an Existing Parent 
    -   Change Tracker Response to New Child of Existing Parent
        -   As child's key value is not set, state will automatically be "Added"
        -   Child's FK value to parent (e.g. Book.AuthorId) is set to parent's key
    -   Change Tracker Response to New Child of Existing Parent
        -   Add child to child collection of existing tracked parent. SaveChanges
        -   Add existing tracked parent to ref property of child. SaveChanges
        -   Set foreign key property in child class to parent's key value. Add & SaveChanges
    -   Beware accidental inserts while inserting child object
        -   Passing a pre-existing entity into its DbSet 
            Add will cause EF Core to try to insert it into the database
        -   Example
            var author = dbContext.Authors.Find(3);
            author.Books.Add(new Books() { Name = "Ramayan"});
            dbContext.Authors.Add(author);  // XXXXXXXXXXX
            // this will try to add author which already has ID set and then DB give error PK duplication
            // correct way in ParentChildInsert.cs

    We can also filter in .Include() by .Include(x => x.Books.Where( b => b.Name == "Akshay").OrderBy( b => b.Title)

-   Filtering & Sorting the Included Data (INCLUDE())
    -   By default, the entire collection is retrieved
    -   You can filter and sort the related data
    -   Long requested feature that finally arrived in EF Core 5!

    _context.Authors.Where(a=>a.LastName.StartsWith("L"))
        .Include(a=>a.Books).ToList()

    _context.Authors.Where(a => a.LastName == "Lerman")
        .Include(a => a.Books).FirstOrDefault()
    
    _context.Authors.Where(a => a.LastName == "Lerman")
        .FirstOrDefault()
        .Include(a => a.Books)

    _context.Authors.Find(1)
            .Include(a=>a.Books)
    
    _context.Authors
        .Include(a=>a.Books).Find(1)

Using Include for Multiple Layers of Relationships

    -   Get books for each author Then get the jackets for each book    
            _context.Authors
                .Include(a => a.Books)
                .ThenInclude(b=>b.BookJackets)
                .ToList();

    -   Get books for each author and Also get the contact info each author
        -   _context.Authors
                .Include(a => a.Books)
                .Include(a=>a.ContactInfo)
                .ToList();
    
    -   Get the jackets for each author's books (But don't get the books)
        -   _context.Authors
            .Include(a=>a.Books.BookJackets).ToList();

    -   Composing many Includes in one query could create performance issues. Monitor your queries!
    -   Include defaults to a single SQL command. Use AsSplitQuery() to  send multiple SQL commands instead.    

    
    -   EF Core Can Only Track Entities Recognized by the DbContext (see ProjectionExplain.cs)
        -   Anonymous types are not tracked
        -   Entities that are properties of an anonymous type are tracked
    
    -   Loading Related Data for Objects Already in Memory
        -   ExplicitLoading.cs
        -   With author object already in memory, load a collection
                _context.Entry(author).Collection(a => a.Books).Load();
        -   With book object already in memory, load a reference (e.g., parent or 1:1)
                _context.Entry(book).Reference(b => b.Author).Load();
        -   Explicit Loading
                Explicitly retrieve related data for objects already in memory
                DbContext.Entry(object).Collection().Load()
                DbContext.Entry(object).Reference().Load()
        -   You can only load from a single object 
            -   Profile to determine if LINQ query would be better performance
        -   Filter loaded data using the Query method
            var happyQuotes = context.Entry(samurai)
                                     .Collection(b => b.Quotes)
                                     .Query()
                                     .Where(q => q.Quote.Contains("Happy")
                                     .ToList();
        -   Note: You can only load from a single object
        -   genearlly - Profile to determine if LINQ query would be better performance
    
        -    var recentAuthors = _context.Authors
                                            .Where(a => a.Books.Any(b => b.Date.Year >= 2015))
                                            .ToList();
            // only retrive authors
            // note - book collections will be empty though we have use in Where Query

        -   EfUpdateRelatedData 

        -   Delete
            -   Remove from an in-memory collection
            -   Set State to Deleted in Change Tracker
            -   Delete from database

        -   Cascade Delete When Dependents Can't Be "Orphaned
            
            void DeleteAnAuthor()
                    {
                        var author = _context.Authors.Find(2);
                        _context.Authors.Remove(author);
                        _context.SaveChanges();
                    }
            Database Enforces Cascade Delete
                Only author is in memory, tracked by EF Core & marked "Deleted"
                Database's cascade delete will take care of books when author is deleted

        -   EF Core Enforces Cascade Deletes
            Any related data that is also tracked will be marked as Deleted 
            along with the principal object
            
        -   It's running the DELETE FROM Books while the books are still in the database.

        -   var author=_context.Authors             // Retrieve an author with only *some few* 
                .Include(a=>a.Books                 // of their books
                .Where(b=>b.Title="XYZ")
                .FirstOrDefault();
                
            _context.Authors.Remove(author);        // Mark the author as deleted
            
            //Entry(author).State=Deleted           //Change tracker will also mark that book as deleted
            //Entry(thatbook).State=Deleted

            _context.SaveChanges() // SaveChanges sends DELETE for that book and DELETE for the author to database
        
            // bit whta for other book that was not loded 
            //database cascade delete any other books  
            //The database will delete any remaining books in that database for the author
_
*/
