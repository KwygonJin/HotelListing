using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.DAL.Entities.Configuration;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(
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