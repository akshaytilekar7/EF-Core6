using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherConsole
{
    public class EfMaytoMany
    {
        PubContext _context = new PubContext();
        public void ConnectExistingArtistAndCoverObjects()
        {
            var artistA = _context.Artists.Find(1);
            var artistB = _context.Artists.Find(2);
            var coverA = _context.Covers.Find(1);
            coverA.Artists.Add(artistA);
            coverA.Artists.Add(artistB);
            _context.SaveChanges();
        }

        public void CreateNewCoverWithExistingArtist()
        {
            var artistA = _context.Artists.Find(1);
            var cover = new Cover { DesignIdeas = "Author has provided a photo" };
            cover.Artists.Add(artistA);
            _context.Covers.Add(cover);
            _context.SaveChanges();
        }

        public void CreateNewCoverAndArtistTogether()
        {
            var newArtist = new Artist { FirstName = "Kir", LastName = "Talmage" };
            var newCover = new Cover { DesignIdeas = "We like birds!" };
            newArtist.Covers.Add(newCover);
            _context.Artists.Add(newArtist);
            _context.SaveChanges();
        }
    }

    public class EfMayToManyDelete
    {
        PubContext _context = new PubContext();

        void UnAssignAnArtistFromACover()
        {
            var coverwithartist = _context.Covers
                .Include(c => c.Artists.Where(a => a.ArtistId == 2))
                .FirstOrDefault(c => c.CoverId == 1);
            //coverwithartist.Artists.RemoveAt(0);
            _context.Artists.Remove(coverwithartist.Artists[0]);
            _context.ChangeTracker.DetectChanges();
            var debugview = _context.ChangeTracker.DebugView.ShortView;
            //_context.SaveChanges();
        }

        void DeleteAnObjectThatsInARelationship()
        {
            var cover = _context.Covers.Find(4);
            _context.Covers.Remove(cover);
            _context.SaveChanges();
        }

        void ReassignACover()
        {
            var coverwithartist4 = _context.Covers
            .Include(c => c.Artists.Where(a => a.ArtistId == 4))
            .FirstOrDefault(c => c.CoverId == 5);

            coverwithartist4.Artists.RemoveAt(0);
            var artist3 = _context.Artists.Find(3);
            coverwithartist4.Artists.Add(artist3);
            _context.ChangeTracker.DetectChanges();


        }
    }
}
