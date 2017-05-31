using System.Data.Entity;
using VideothekData.Models;

namespace VideothekData
{
    public class VideothekContext : DbContext
    {
        public VideothekContext() : base("VideothekDb")
        { }

        //public Tabelle<Klassendefinition> TabellenName { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> Memberships { get; set; }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                        .HasMany<Genre>(m => m.Genres)
                        .WithMany(g => g.Movies)
                        .Map(map => {
                            map.MapLeftKey("MovieId");
                            map.MapRightKey("GenreId");
                            map.ToTable("MovieGenre");
                        });
        }
    }
}
