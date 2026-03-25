using ECommerce.Common;
using System.Linq.Expressions;

namespace ECommerce.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllGenericAsync(Expression<Func<T, bool>> expression = null!,
                                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
                                                        Func<IQueryable<T>, IQueryable<T>> include = null!,
                                                        bool trackChanges = false);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagedResult<T>> GetAllPagedAsync(PaginationParameters? paginationParameters = null, BaseFilterParameters? filterParameters = null);
        Task<T?> GetByIdAsync(int id);
        void Add(T entity);
        void Delete(T entity);
    }
}
