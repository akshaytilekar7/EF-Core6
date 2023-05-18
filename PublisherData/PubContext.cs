using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        // needed for CONSOLE NOT FOR WEB 
        // uncomment for Console
        public PubContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            SavingChanges += SavingChangesHandler;

        }

        // needed for WEB
        // comment for console
        public PubContext(DbContextOptions<PubContext> options) : base(options)
        {
        }
        private void SavingChangesHandler(object? sender, SavingChangesEventArgs e)
        {
            UpdateAuditData();
        }

        private void UpdateAuditData()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is Author))
            {
                entry.Property("LastUpdated").CurrentValue = DateTime.Now;
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<BookWithCoverView> BookWithCoverView { get; set; }

        // needed for CONSOLE NOT FOR WEB 
        // uncomment for Console 
        // now no need to comment uncomment we have added check "optionsBuilder.IsConfigured"
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var connectionString = "Server=(local);Database=TempDatabase1234;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookWithCoverView>().HasNoKey()
                .ToView(nameof(BookWithCoverView));

            base.OnModelCreating(builder);
        }
    }
}
