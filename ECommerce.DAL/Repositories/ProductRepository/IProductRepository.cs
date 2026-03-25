using ECommerce.Common;

namespace ECommerce.DAL

{
    public interface IProductRepository : IGenericRepository<Product>
    {
            Task<IEnumerable<Product>> GetAllWithCategoriesAsync();
             Task<Product?> GetByIdWithCategoryAsync(int id);
             Task<PagedResult<Product>> GetAllPagedAsync(PaginationParameters? paginationParameters = null, ProductFilterParameters? filterParameters = null);
    }
}
