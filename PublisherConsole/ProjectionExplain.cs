using PublisherData;

namespace PublisherConsole
{
    internal class ProjectionExplain
    {
        PubContext _context;
        public ProjectionExplain()
        {
            _context = new PubContext();
        }
        public void Projections()
        {
            var unknownTypes = _context.Authors
                .Select(a => new
                {
                    AuthorId = a.Id,
                    Name = a.FirstName.First() + "" + a.LastName,
                    Books = a.Books
                }).ToList();

            var debugview = _context.ChangeTracker.DebugView.ShortView;
            /*
                -   anonymous type cant be TRACK
                    but of er dorectly refer property like as Books it will track
                Book {BookId: 1} Unchanged FK {AuthorId: 3}
                Book {BookId: 2} Unchanged FK {AuthorId: 3}
                Book {BookId: 3} Unchanged FK {AuthorId: 6}
                Book {BookId: 4} Unchanged FK {AuthorId: 6}
             
            */
        }
    }
}
