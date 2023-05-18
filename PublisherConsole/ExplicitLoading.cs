using PublisherData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherConsole
{
    internal class ExplicitLoading
    {
        PubContext _context = new PubContext();
        public void ExplicitLoadCollection()
        {
            var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
            _context.Entry(author).Collection(a => a.Books).Load();

        }

        public void ExplicitLoadReference()
        {
            var book = _context.Books.FirstOrDefault();
            _context.Entry(book).Reference(b => b.Author).Load();
        }

        void FilterUsingRelatedData()
        {
            var recentAuthors = _context.Authors
                .Where(a => a.Books.Any(b => b.PublishDate.Year >= 2015))
                .ToList();
            // only retrive authors
            // note - book collections will be empty though we have use in Where Query
        }

    }
}
