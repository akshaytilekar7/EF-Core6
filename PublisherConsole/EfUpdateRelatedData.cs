using Microsoft.EntityFrameworkCore;
using PublisherData;

namespace PublisherConsole
{
    internal class EfUpdateRelatedData
    {
        PubContext _context;
        public EfUpdateRelatedData()
        {
            _context = new PubContext();
        }
        internal void ModifyingRelatedDataWhenTracked()
        {
            var author = _context.Authors.Include(a => a.Books)
                                 .FirstOrDefault(a => a.FirstName == "Akshata");
            author.Books[0].BasePrice = (decimal)10.00; // OR
            //author.Books.Remove(author.Books[1]);
            _context.ChangeTracker.DetectChanges();
            var state = _context.ChangeTracker.DebugView.ShortView;

            /*
            For Remove author.Books.Remove(author.Books[1]);
            Author {Id: 3} Unchanged
                Book {BookId: 1} Unchanged FK {AuthorId: 3}
                Book {BookId: 2} Deleted FK {AuthorId: 3}

            // For  author.Books[0].BasePrice = (decimal)10.00;
            Author {Id: 3} Unchanged
                Book {BookId: 1} Modified FK {AuthorId: 3}
                Book {BookId: 2} Unchanged FK {AuthorId: 3}
            */

        }

        internal void ModifyingRelatedDataWhenNotTracked()
        {
            var author = _context.Authors.Include(a => a.Books)
                                 .FirstOrDefault(a => a.FirstName == "Akshata");
            author.Books[0].BasePrice = (decimal)12.00;

            var newContext = new PubContext();
            newContext.Books.Update(author.Books[0]);
            var state = newContext.ChangeTracker.DebugView.ShortView;

            /*
                Author {Id: 3} Modified
                Book {BookId: 1} Modified FK {AuthorId: 3}
                Book {BookId: 2} Modified FK {AuthorId: 3}

            */
        }


        internal void ModifyingRelatedDataWhenNotTracked_2()
        {
            var author = _context.Authors.Include(a => a.Books)
                                 .FirstOrDefault(a => a.FirstName == "Akshata");
            author.Books[0].BasePrice = (decimal)12.00;

            var newContext = new PubContext();
            // newContext.Books.Update(author.Books[0]); INSTEAD WE CAN USE
            newContext.Entry(author.Books[0]).State = EntityState.Modified;
            var state = newContext.ChangeTracker.DebugView.ShortView;
            newContext.SaveChanges();
            /*
                Book {BookId: 1} Modified FK {AuthorId: 3}
            */
        }

    }
}
