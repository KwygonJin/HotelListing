using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace HotelListing.DAL.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    );

    Task<T> GetAsync(Expression<Func<T, bool>> expression = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    );

    Task InsertAsync(T entity);

    Task InsertRangeAsync(IEnumerable<T> entities);

    Task DeleteAsync(int id);

    void DeleteRange(IEnumerable<T> entities);

    void Update(T entity);
}