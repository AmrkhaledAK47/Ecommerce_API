using ECommerce.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppContext context) : base(context) { }

        public async Task<PagedResult<Product>> GetAllPagedAsync(PaginationParameters? paginationParameters = null, ProductFilterParameters? filterParameters = null)
        {
            var query = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .AsQueryable();

            query = ApplyBaseFilter(query, filterParameters);

            if (filterParameters?.MinCount is not null)
            {
                query = query.Where(p => p.Count >= filterParameters.MinCount);
            }

            if (filterParameters?.MaxCount is not null)
            {
                query = query.Where(p => p.Count <= filterParameters.MaxCount);
            }

            if (filterParameters?.minPrice is not null)
            {
                query = query.Where(p => p.Price >= filterParameters.minPrice);
            }

            if (filterParameters?.maxPrice is not null)
            {
                query = query.Where(p => p.Price <= filterParameters.maxPrice);
            }

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

            return new PagedResult<Product>
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

        public async Task<IEnumerable<Product>> GetAllWithCategoriesAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdWithCategoryAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
