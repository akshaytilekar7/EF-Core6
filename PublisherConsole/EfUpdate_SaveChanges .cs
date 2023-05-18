using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class EfUpdate_SaveChanges
    {
        public void Update()
        {
            using var sameDbContext = new PubContext();
            var author = sameDbContext.Authors.Find(3);
            author.FirstName = "new name";
            sameDbContext.SaveChanges();
            /*
              Author {Id: 3} Unchanged
              Id: 3 PK
              FirstName: 'new name' Originally 'Akshata'
              LastName: 'Kedari'
              Books: []

            */

        }

        public void Update2()
        {
            Author? author = null;
            using (var dbContextLongScope = new PubContext())
            {
                author = dbContextLongScope.Authors.Find(3);
            }

            using (var dbContextLongScope = new PubContext())
            {
                author.FirstName = "new name 123";
                dbContextLongScope.SaveChanges();
            }
            /*
            In this case nothing in changetracker as new DB connetext
            */

        }

        public void Update3()
        {
            Author? author = null;
            using (var dbContextLongScope = new PubContext())
            {
                author = dbContextLongScope.Authors.Find(3);
            }

            using (var dbContextLongScope = new PubContext())
            {
                author.FirstName = "new name 123";
                dbContextLongScope.Update(author);
                dbContextLongScope.SaveChanges();
            }
            /*
             
            Author {Id: 3} Modified
              Id: 3 PK
              FirstName: 'new name 123' Modified
              LastName: 'Kedari' Modified
              Books: []

            */

        }

        public void Update4()
        {
            Author? author = null;
            using (var dbContextLongScope = new PubContext())
            {
                author = dbContextLongScope.Authors.Find(3);
            }

            using (var dbContextLongScope = new PubContext())
            {
                author.FirstName = "new name 123 qeqwe ";
                dbContextLongScope.Authors.Update(author);
                dbContextLongScope.SaveChanges();
            }
            /*
             Author {Id: 3} Modified
                  Id: 3 PK
                  FirstName: 'new name 123 qeqwe ' Modified
                  LastName: 'Kedari' Modified
                  Books: []

            */

        }

        public void Update5()
        {
            Author? author = null;
            using (var sameDbContext = new PubContext())
            {
                author = sameDbContext.Authors.Find(3);

                author.FirstName = "new name 123 qeqwe ";
                sameDbContext.Update(author);
                sameDbContext.SaveChanges();

                /*
                 Author {Id: 3} Modified
                      Id: 3 PK
                      FirstName: 'new name 123 qeqwe ' Modified Originally 'new name 123'
                      LastName: 'Kedari' Modified
                      Books: []

                */
            }
        }

        public void Update6()
        {
            Author? author = null;
            using (var sameDbContext = new PubContext())
            {
                author = sameDbContext.Authors.Find(3);

                author.FirstName = "new name 123 qeqwe ";
                sameDbContext.Authors.Update(author);
                sameDbContext.SaveChanges();

                /*
                 Author {Id: 3} Modified
                      Id: 3 PK
                      FirstName: 'new name 123 qeqwe ' Modified
                      LastName: 'Kedari' Modified
                      Books: []
                */
            }
        }


    }
}
