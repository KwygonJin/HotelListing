using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {}

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                    Rating = 4.5,
                    Price = 500
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Hotel Jamaica",
                    Address = "Jamaica",
                    CountryId = 2,
                    Rating = 4.8,
                    Price = 4000
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hotel Bahamas",
                    Address = "Bahamas",
                    CountryId = 3,
                    Rating = 5,
                    Price = 7000
                }
             );
        }
    }
}
