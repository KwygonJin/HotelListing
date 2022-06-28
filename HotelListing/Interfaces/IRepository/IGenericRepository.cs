using HotelListing.DTO;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace HotelListing.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );
        Task<IPagedList<T>> GetPagedAsync(
            RequestParams requestParams,
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
}
