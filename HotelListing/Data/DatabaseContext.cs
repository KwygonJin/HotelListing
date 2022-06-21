using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Ukraine",
                    ShortName = "UA"
                },
                new Country
                {
                    Id = 2,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                 new Country
                 {
                     Id = 3,
                     Name = "Bahamas",
                     ShortName = "BS"
                 }
             );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Hotel Ukraine",
                    Address = "Lviv",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Hotel Jamaica",
                    Address = "Jamaica",
                    CountryId = 2,
                    Rating = 4.8
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hotel Bahamas",
                    Address = "Bahamas",
                    CountryId = 3,
                    Rating = 5
                }
             ); 
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
