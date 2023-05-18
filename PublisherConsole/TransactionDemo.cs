using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PublisherData;

namespace PublisherConsole
{
    internal class TransactionDemo
    {

        PubContext _context = new PubContext(); //existing database
        /* cancel booking
            -   2 rask involves
                a.  Update Comment of entity (here we use LastName fro Demo purpose)
                b.  Delete singl Book

        */
        void CancelBookWithDefaultTransaction_BAD_INCASEOFERROR(int bookid)
        {
            // one transaction 
            var artists = _context.Artists.Where(a => a.Covers.Any(cover => cover.BookId == bookid)).ToList();
            foreach (var artist in artists)
                artist.LastName += Environment.NewLine + $"Assigned book { bookid} was cancelled on { DateTime.Today.Date} ";

            // new transaction 
            //by default, raw sql methods are not in transactions
            _context.Database.ExecuteSqlInterpolated($"Delete from books where bookid={bookid}");

            // actually if any fails we want to rollback
            // but here we cant rollback as they have seprate Transaction 
            _context.SaveChanges();
        }

        void CancelBookWithCustomTransaction(int bookid)
        {
            using var transaction = _context.Database.BeginTransaction(); // MAIN LINE ONE TRANSACTION ONLY
            try
            {
                var artists = _context.Artists.Where(a => a.Covers.Any(cover => cover.BookId == bookid)).ToList();
                foreach (var artist in artists)
                    artist.LastName += Environment.NewLine + $"Assigned book { bookid} was cancelled on { DateTime.Today.Date} ";                                                                                                                         //by default, raw sql methods execute in their own transaction immediately

                _context.Database.ExecuteSqlInterpolated($"Delete from books where bookid={bookid}");

                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                // TODO: Handle failure
            }
        }
    }
}
