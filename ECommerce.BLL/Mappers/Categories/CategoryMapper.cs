using ECommerce.DAL;

namespace ECommerce.BLL
{
    public class CategoryMapper : ICategoryMapper
    {
        public IEnumerable<CategoryDTO> MapToCategoryDTOs(IEnumerable<Category> categories)
        {
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
        public CategoryDTO MapToCategoryDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public Category MapToCategory(CategoryDTO categoryDTO)
        {
            return new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            };
        }
        public CategoryReadDetailsDTO MapToCategoryReadDetailsDTO(Category category)
        {
            return new CategoryReadDetailsDTO
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products?.Select(p => new ProductReadDetailsDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Count = p.Count,
                    Description = p.Description,
                    CategoryName = category.Name,
                    ImageUrl = p.ImageUrl,
                    ExpiryDate = p.ExpiryDate
                }).ToList()
            };
        }
    }
}
