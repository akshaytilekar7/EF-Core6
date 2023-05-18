using PublisherData;

namespace PublisherConsole
{
    public class Ef_Remove
    {
        public void RemoveByNullSameDbContextNotWorkingAsExpected()
        {
            using var dbContextLongScope = new PubContext();
            var x = dbContextLongScope.Authors.Find(2);

            if (x != null)
                dbContextLongScope.Remove(x);

            dbContextLongScope.SaveChanges();

            /*
             Author {Id: 2} Deleted
                  Id: 2 PK
                  FirstName: 'Pragati'
                  LastName: 'Tilekar'
                  Books: []
             */
        }

        public void Remove()
        {
            PublisherDomain.Author? x = null;
            using (var dbShort1 = new PubContext())
            {
                x = dbShort1.Authors.Find(1);
                x = null;
                dbShort1.SaveChanges();
            }

            using (var dbShort2 = new PubContext())
            {
                if (x != null)
                {
                    x = null;
                    dbShort2.SaveChanges();
                }
            }

        }

    }
}
