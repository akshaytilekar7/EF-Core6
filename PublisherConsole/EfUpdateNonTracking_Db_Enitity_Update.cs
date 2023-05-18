using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class EfUpdateNonTracking_Db_Enitity_Update
    {
        public void CoordinatedRetrieveAndUpdateAuthor()
        {
            var author = FindThatAuthor(3);
            if (author != null)
            {
                author.FirstName = "Julia";
                SaveThatAuthor(author);
            }
        }
        private Author FindThatAuthor(int authorId)
        {
            using var firstDbContext = new PubContext();
            return firstDbContext.Authors.Find(authorId);
        }
        private void SaveThatAuthor(Author author)
        {
            using var secondDbContext = new PubContext();
            secondDbContext.Authors.Update(author);
            secondDbContext.ChangeTracker.DetectChanges();
            secondDbContext.SaveChanges();
            /* change tracker
              Author {Id: 3} Modified
              Id: 3 PK
              FirstName: 'Julia' Modified
              LastName: 'Kedari' Modified
              Books: []
            */
            // As new context no idea of history so update all properties
            // query include all properties such as FirstName and LastName
        }
    }
}
