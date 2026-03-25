using ECommerce.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
namespace ECommerce.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppContext _context;

        public GenericRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllGenericAsync(Expression<Func<T, bool>> expression = null!,
                                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
                                                        Func<IQueryable<T>, IQueryable<T>> include = null!,
                                                        bool trackChanges = false)
        {
            IQueryable<T> query = _context.Set<T>();

            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (include is not null)
            {
                query = include(query);
            }
            if (orderBy is not null)
            {
                query = orderBy(query);
            }
            if (trackChanges == false)
            {
                query = query.AsNoTracking();
            }
            return await query.ToListAsync();
        }
        public async Task<PagedResult<T>> GetAllPagedAsync(PaginationParameters? paginationParameters = null, BaseFilterParameters? filterParameters = null)
        {
            var query = _context.Set<T>().AsNoTracking();
            query = ApplyBaseFilter(query, filterParameters);
            
            var totalCount = await query.CountAsync();
            var pageSize = paginationParameters?.PageSize ?? 8;
            var pageNumber = paginationParameters?.PageNumber ?? 1;

            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 20);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (totalPages > 0)
            {
                pageNumber = Math.Min(pageNumber, totalPages);
            }
            else
            {
                pageNumber = 1;
            }

            var items = await query
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return new PagedResult<T>
                {
                Items = items,
                MetaData = new PaginationMetaData
                {
                    TotalCount = totalCount,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = totalPages
                }
            };
        }

        protected static IQueryable<T> ApplyBaseFilter(IQueryable<T> query, BaseFilterParameters? filterParameters)
        {
            if (filterParameters == null)
            {
                return query;
            }

            if (!string.IsNullOrWhiteSpace(filterParameters.Search))
            {
                var search = filterParameters.Search.Trim();
                var stringProperties = typeof(T)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.PropertyType == typeof(string))
                    .ToArray();

                if (stringProperties.Length > 0)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    Expression? body = null;

                    foreach (var property in stringProperties)
                    {
                        var propertyAccess = Expression.Property(parameter, property);
                        var notNull = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
                        var contains = Expression.Call(propertyAccess, nameof(string.Contains), Type.EmptyTypes, Expression.Constant(search));
                        var condition = Expression.AndAlso(notNull, contains);
                        body = body == null ? condition : Expression.OrElse(body, condition);
                    }

                    if (body != null)
                    {
                        var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);
                        query = query.Where(predicate);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(filterParameters.SortBy))
            {
                var sortProperty = typeof(T).GetProperty(
                    filterParameters.SortBy,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (sortProperty != null)
                {
                    query = filterParameters.SortDescending
                        ? query.OrderByDescending(e => EF.Property<object>(e, sortProperty.Name))
                        : query.OrderBy(e => EF.Property<object>(e, sortProperty.Name));
                }
            }

            return query;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

    }
}
