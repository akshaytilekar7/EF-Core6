using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class EfUpdateTracking_Db_Update
    {
        public void SaveThatAuthorTrackable()
        {
            using var dbContextLongScope = new PubContext();
            var author = dbContextLongScope.Authors.Find(3);
            author.FirstName = "new name";
            dbContextLongScope.ChangeTracker.DetectChanges();
            dbContextLongScope.SaveChanges();
            /* change tracker
             Author {Id: 3} Modified
              Id: 3 PK
              FirstName: 'new name' Modified Originally 'Akshata'
              LastName: 'Kedari'
              Books: []
            */
            // As no new context , dbcontext has idea of history so update only updated properties
            // query include only 1 update set property FiestName

        }


        // ONLY IN ABOVE CASE QUERY WILL BE OPTIMIZED ALL OTHER ANY COMNIATION WILL RESULT INTO FULL PROPERT UPDATE

    }
}
