using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    internal class ParentChildInsert
    {
        public PubContext _context { get; }

        public ParentChildInsert(PubContext pubContext)
        {
            _context = pubContext;
        }

        void InsertNewAuthorWithNewBook()
        {
            var author = new Author { FirstName = "Lynda", LastName = "Rutledge" };
            author.Books.Add(new Book
            {
                Title = "West With Giraffes",
                PublishDate = new DateTime(2021, 2, 1)
            });
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        void InsertNewAuthorWith2NewBooks()
        {
            var author = new Author { FirstName = "Don", LastName = "Jones" };
            author.Books.AddRange(new List<Book> {
                new Book {Title = "The Never", PublishDate = new DateTime(2019, 12, 1) },
                new Book {Title = "Alabaster", PublishDate = new DateTime(2019,4,1)}
            });
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        void AddNewBookToExistingAuthorInMemory()
        {
            var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
            if (author != null)
            {
                var bk = new Book { Title = "Wool", PublishDate = new DateTime(2012, 1, 1) };
                author.Books.Add(bk);
            }
            _context.SaveChanges();
        }

        void AddNewBookToExistingAuthorInMemoryViaBook()
        {
            var book = new Book
            {
                Title = "Shift",
                PublishDate = new DateTime(2012, 1, 1),
                AuthorId = 5
            };
            // book.Author = _context.Authors.Find(5); // no call to db as AuthorId
            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
}
