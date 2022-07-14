using HotelListing.DAL.EF;
using HotelListing.DAL.Entities;
using HotelListing.DAL.Interfaces;

namespace HotelListing.DAL.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private IGenericRepository<Country> _countries;
    private IGenericRepository<Hotel> _hotels;

    public UnitOfWork(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }

    public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);
    public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}