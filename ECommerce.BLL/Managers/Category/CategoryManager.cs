using ECommerce.Common;
using ECommerce.DAL;
using FluentValidation;

namespace ECommerce.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CategoryDTO> _categoryValidator;
        private readonly ICategoryMapper _categoryMapper;
        private readonly IErrorMapper _errorMapper;
        public CategoryManager(IUnitOfWork unitOfWork, IValidator<CategoryDTO> categoryValidator, ICategoryMapper categoryMapper, IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _categoryValidator = categoryValidator;
            _categoryMapper = categoryMapper;
            _errorMapper = errorMapper;
        }

        public async Task<GeneralResult<IEnumerable<CategoryDTO>>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var categoryDTOs = _categoryMapper.MapToCategoryDTOs(categories);
            return GeneralResult<IEnumerable<CategoryDTO>>.SuccessResult(categoryDTOs);
        }
        public async Task<GeneralResult<CategoryReadDetailsDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdWithProductsAsync(id);
            if (category == null)
            {
                return GeneralResult<CategoryReadDetailsDTO>.NotFound($"Category with ID {id} not found.");
            }
            var categoryReadDetailsDTO = _categoryMapper.MapToCategoryReadDetailsDTO(category);
            return GeneralResult<CategoryReadDetailsDTO>.SuccessResult(categoryReadDetailsDTO);
        }
        public async Task<GeneralResult<CategoryDTO>> CreateCategoryAsync(CategoryDTO category)
        {
            var validationResult = await _categoryValidator.ValidateAsync(category);

            if (!validationResult.IsValid)
            {
                return GeneralResult<CategoryDTO>.FailureResult(_errorMapper.MapToErrors(validationResult), "Validation failed.");
            }
            
            var categoryEntity = _categoryMapper.MapToCategory(category);
            _unitOfWork.CategoryRepository.Add(categoryEntity);
            await _unitOfWork.SaveAsync();

            var createdCategory = _categoryMapper.MapToCategoryDTO(categoryEntity);
            return GeneralResult<CategoryDTO>.SuccessResult(createdCategory, "Category created successfully.");
        }
        public async Task<GeneralResult> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return GeneralResult.NotFound($"Category with ID {id} not found.");
            }
            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Category deleted successfully.");
        }
        public async Task<GeneralResult> EditCategoryAsync(CategoryDTO category)
        {
            var validationResult = await _categoryValidator.ValidateAsync(category);

            if (!validationResult.IsValid) {
                return GeneralResult.FailureResult(_errorMapper.MapToErrors(validationResult), "Validation failed.");
            }

            var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(category.Id);

            if (existingCategory is null)
            {
                return GeneralResult.NotFound($"Category with ID {category.Id} not found.");
            }

            existingCategory.Name = category.Name;
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Category updated successfully.");
        }
    }
}
