using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PublisherConsole
{
    public class EfStroreProcView
    {
        PubContext _context = new PubContext();

        public void DeleteCoverStroeProc(int coverId)
        {
            int rowCount = _context.Database.ExecuteSqlRaw("DeleteCover {0}", coverId);
            Console.WriteLine("Affeted rows :"+ rowCount);
        }

        public void ViewCheck()
        {
            var authorartists = _context.BookWithCoverView.ToList();
            var oneauthorartists = _context.BookWithCoverView.FirstOrDefault();
            var Kauthorartists = _context.BookWithCoverView.Where(a => !string.IsNullOrEmpty(a.Title)).ToList();
            var debugView = _context.ChangeTracker.DebugView.ShortView;
            Console.WriteLine("ViewCheck rows :" + Kauthorartists.Count);
        }

        public void RawSqlStoredProc()
        {
            var authors = _context.Authors
                        .FromSqlRaw("AuthorsPublishedinYearRange {0}, {1}", 1999, 2050)
                        .ToList();

            Console.WriteLine("RawSqlStoredProc rows :" + authors.Count());

        }

        public void InterpolatedSqlStoredProc()
        {
            int start = 1990;
            int end = 2015;
            var authors = _context.Authors
                            .FromSqlInterpolated($"AuthorsPublishedinYearRange {start}, {end}")
                            .ToList();
            Console.WriteLine("InterpolatedSqlStoredProc rows :" + authors.Count());

        }

        public void SimpleRawSQL()
        {
            var authors = _context.Authors.FromSqlRaw("select * from authors").OrderBy(a => a.LastName).ToList();
        }

        public void FormattedRawSql_Safe()
        {
            var lastnameStart = "L";
            var authors = _context.Authors
                .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart)
                .OrderBy(a => a.LastName).TagWith("Formatted_Safe").ToList();
        }

        public void StringFromInterpolated_Safe()
        {
            var lastnameStart = "L";
            var authors = _context.Authors
                .FromSqlInterpolated($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
            .OrderBy(a => a.LastName).TagWith("Interpolated_Safe").ToList();
        }

        public void ConcatenatedRawSql_Unsafe()
        {
            var lastnameStart = "L";
            var authors = _context.Authors
                .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '" + lastnameStart + "%'")
                .OrderBy(a => a.LastName).TagWith("Concatenated_Unsafe").ToList();
        }

        public void FormattedRawSql_Unsafe()
        {
            var lastnameStart = "L";
            var sql = String.Format("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart);
            var authors = _context.Authors.FromSqlRaw(sql)
                .OrderBy(a => a.LastName).TagWith("Formatted_Unsafe").ToList();
        }

        public void StringFromInterpolated_Unsafe()
        {
            var lastnameStart = "L";
            string sql = $"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'";
            var authors = _context.Authors.FromSqlRaw(sql)
                .OrderBy(a => a.LastName).TagWith("Interpolated_Unsafe").ToList();
        }
        public void StringFromInterpolated_StillUnsafe()
        {
            var lastnameStart = "L";
            var authors = _context.Authors
                .FromSqlRaw($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
                .OrderBy(a => a.LastName).TagWith("Interpolated_StillUnsafe").ToList();
        }


    }
}
