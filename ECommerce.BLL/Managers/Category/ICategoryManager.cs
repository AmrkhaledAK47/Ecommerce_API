using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface ICategoryManager
    {
        Task<GeneralResult<IEnumerable<CategoryDTO>>> GetAllCategoriesAsync();
        Task<GeneralResult<CategoryReadDetailsDTO>> GetCategoryByIdAsync(int id);
        Task<GeneralResult<CategoryDTO>> CreateCategoryAsync(CategoryDTO category);
        Task<GeneralResult> EditCategoryAsync(CategoryDTO category);
        Task<GeneralResult> DeleteCategoryAsync(int id);
    }
}
