using ECommerce.DAL;

namespace ECommerce.BLL
{
    public interface ICategoryMapper
    {
        IEnumerable<CategoryDTO> MapToCategoryDTOs(IEnumerable<Category> categories);
        CategoryDTO MapToCategoryDTO(Category category);
        Category MapToCategory(CategoryDTO categoryDTO);
        CategoryReadDetailsDTO MapToCategoryReadDetailsDTO(Category category);
    }
}