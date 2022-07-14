using HotelListing.DAL.Entities;
using HotelListing.DAL.Entities.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.DAL.EF;

public class DatabaseContext : IdentityDbContext<ApiUser>
{
    public DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Hotel> Hotels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new HotelConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
    }
}