using HotelListing.DAL.Entities;

namespace HotelListing.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Country> Countries { get; }

    IGenericRepository<Hotel> Hotels { get; }

    Task SaveAsync();
}