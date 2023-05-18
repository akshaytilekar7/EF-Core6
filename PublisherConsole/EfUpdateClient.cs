using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class EfUpdateClient
    {
        public static void Main()
        {
            PubContext _context = new PubContext();

            _context.Authors.Add(new Author() { FirstName = "Akshay", LastName = "Tilekar" });
            _context.Authors.Add(new Author() { FirstName = "Pragati", LastName = "Tilekar" });

            var books = new List<Book>()
            {
                new Book() { Title = "AAA" , BasePrice = 50, PublishDate = DateTime.Now , Cover = new Cover() { DesignIdeas = " DesignIdeas 1" } },
                new Book() { Title = "ABBB", BasePrice = 100, PublishDate = DateTime.Now.AddDays(-500), Cover = new Cover() { DesignIdeas = " DesignIdeas 2" }  }
            };
            _context.Authors.Add(
                new Author() { FirstName = "Akshata", LastName = "Kedari", Books = books });

            _context.SaveChanges();


            EfStroreProcView objProc = new EfStroreProcView();

            objProc.DeleteCoverStroeProc(1);
            objProc.ViewCheck();
            objProc.InterpolatedSqlStoredProc();

            Console.ReadLine();

            return;


            //EfUpdateNonTracking_Db_Enitity_Update obj1 = new EfUpdateNonTracking_Db_Enitity_Update();
            //obj1.CoordinatedRetrieveAndUpdateAuthor();

            //EfUpdateTracking_Db_Update obj2 = new EfUpdateTracking_Db_Update();
            //obj2.SaveThatAuthorTrackable();

            //Ef_Remove ef_Remove = new Ef_Remove();
            //// ef_Remove.RemoveByNullSameDbContextNotWorkingAsExpected();
            //ef_Remove.Remove();

            //EfUpdate_SaveChanges  efUpdate_SaveChanges = new EfUpdate_SaveChanges();
            //efUpdate_SaveChanges.Update5();
            //efUpdate_SaveChanges.Update6();

            //ProjectionExplain projectionExplain = new ProjectionExplain();
            //projectionExplain.Projections();


            EfUpdateRelatedData efUpdateRelatedData = new EfUpdateRelatedData();
            efUpdateRelatedData.ModifyingRelatedDataWhenTracked();
            efUpdateRelatedData.ModifyingRelatedDataWhenNotTracked();
            efUpdateRelatedData.ModifyingRelatedDataWhenNotTracked_2();

        }
    }
}
