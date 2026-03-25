using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface IProductManager
    {
        Task<GeneralResult<IEnumerable<ProductReadAllDTO>>> GetAllProductsAsync();
        Task<GeneralResult<PagedResult<ProductReadAllDTO>>> GetAllPagedProductsAsync(PaginationParameters? paginationParameters = null, ProductFilterParameters? filterParameters = null);
        Task<GeneralResult<IEnumerable<ProductReadAllDTO>>> GetProductsByCategoryAsync(int categoryId);
        Task<GeneralResult<ProductReadDetailsDTO>> GetProductByIdAsync(int id);
        Task<GeneralResult<ProductEditDTO>> GetProductForEditAsync(int id);
        Task<GeneralResult<ProductReadDetailsDTO>> CreateProductAsync(ProductCreateDTO product);
        Task<GeneralResult<IEnumerable<ProductReadAllDTO>>> SearchProductsByNameAsync(string name);
        Task<GeneralResult> EditProductAsync(ProductEditDTO product);
        Task<GeneralResult> DeleteProductAsync(int id);
    }
}
