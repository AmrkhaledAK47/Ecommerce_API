using ECommerce.Common;
using ECommerce.DAL;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
namespace ECommerce.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductCreateDTO> _createValidator;
        private readonly IValidator<ProductEditDTO> _editValidator;
        private readonly IProductMapper _productMapper;
        private readonly IErrorMapper _errorMapper;
        public ProductManager(IUnitOfWork unitOfWork, IValidator<ProductCreateDTO> createValidator, IValidator<ProductEditDTO> editValidator, IProductMapper productMapper, IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _editValidator = editValidator;
            _productMapper = productMapper;
            _errorMapper = errorMapper;
        }
        public async Task<GeneralResult<IEnumerable<ProductReadAllDTO>>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithCategoriesAsync();
            var productDTOs = _productMapper.MapToProductReadAllDTOs(products);
            return GeneralResult<IEnumerable<ProductReadAllDTO>>.SuccessResult(productDTOs, "Products retrieved successfully.");
        }
        public async Task<GeneralResult<PagedResult<ProductReadAllDTO>>> GetAllPagedProductsAsync(PaginationParameters? paginationParameters = null, ProductFilterParameters? filterParameters = null)
        {
            var pagedProducts = await _unitOfWork.ProductRepository.GetAllPagedAsync(paginationParameters, filterParameters);
            var productDTOs = _productMapper.MapToProductReadAllDTOs(pagedProducts.Items);
            var pagedResultDTO = new PagedResult<ProductReadAllDTO>
            {
                Items = productDTOs,
                MetaData = pagedProducts.MetaData
            };
            return GeneralResult<PagedResult<ProductReadAllDTO>>.SuccessResult(pagedResultDTO, "Products retrieved successfully.");
        }
        public async Task<GeneralResult<IEnumerable<ProductReadAllDTO>>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithCategoriesAsync();
            var filteredProducts = products.Where(p => p.CategoryId == categoryId);
            var productDTOs = _productMapper.MapToProductReadAllDTOs(filteredProducts);
            return GeneralResult<IEnumerable<ProductReadAllDTO>>.SuccessResult(productDTOs, "Products retrieved successfully.");
        }
        public async Task<GeneralResult<ProductReadDetailsDTO>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);
            if (product == null)
            {
                return GeneralResult<ProductReadDetailsDTO>.NotFound($"Product with ID {id} not found.");
            }
            var productDTO = _productMapper.MapToProductReadDetailsDTO(product);
            return GeneralResult<ProductReadDetailsDTO>.SuccessResult(productDTO, "Product retrieved successfully.");
        }
        public async Task<GeneralResult<ProductEditDTO>> GetProductForEditAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return GeneralResult<ProductEditDTO>.NotFound($"Product with ID {id} not found.");
            }
            var productDTO = _productMapper.MapToProductEditDTO(product);
            return GeneralResult<ProductEditDTO>.SuccessResult(productDTO, "Product retrieved successfully.");
        }
        public async Task<GeneralResult<ProductReadDetailsDTO>> CreateProductAsync(ProductCreateDTO product)
        {
            var validationResult = await _createValidator.ValidateAsync(product);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapToErrors(validationResult);
                return GeneralResult<ProductReadDetailsDTO>.FailureResult(errors, "Product creation failed due to validation errors.");
            }

            var entity = _productMapper.MapToProductEntity(product);
            _unitOfWork.ProductRepository.Add(entity);
            await _unitOfWork.SaveAsync();

            var entityWithCategory = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(entity.Id);
            if (entityWithCategory == null)
            {
                return GeneralResult<ProductReadDetailsDTO>.NotFound($"Product with ID {entity.Id} not found.");
            }

            var productDTO = _productMapper.MapToProductReadDetailsDTO(entityWithCategory);
            return GeneralResult<ProductReadDetailsDTO>.SuccessResult(productDTO, "Product created successfully.");
        }
        public async Task<GeneralResult> EditProductAsync(ProductEditDTO product)
        {
            var validationResult = await _editValidator.ValidateAsync(product);

            if (!validationResult.IsValid) {
                var errors = _errorMapper.MapToErrors(validationResult);
                return GeneralResult.FailureResult(errors, "Product update failed due to validation errors.");
            }

            var entity = await _unitOfWork.ProductRepository.GetByIdAsync(product.Id);
            if (entity == null)
            {
                return GeneralResult.NotFound($"Product with ID {product.Id} not found.");
            }
            _productMapper.MapToExistingProductEntity(product, entity);
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Product updated successfully.");
        }
        public async Task<GeneralResult> DeleteProductAsync(int id)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return GeneralResult.NotFound($"Product with ID {id} not found.");
            }
            _unitOfWork.ProductRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
            return GeneralResult.SuccessResult("Product deleted successfully.");
        }
        public async Task<GeneralResult<IEnumerable<ProductReadAllDTO>>> SearchProductsByNameAsync(string name)
        {
            var normalizedName = name?.Trim();
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                return GeneralResult<IEnumerable<ProductReadAllDTO>>.NotFound();
            }

            var products = await _unitOfWork.ProductRepository.GetAllGenericAsync(
                expression: p => EF.Functions.Like(p.Title, $"%{normalizedName}%"),
                include: query => query.Include(p => p.Category));

            var productDTOs = _productMapper.MapToProductReadAllDTOs(products);
            return GeneralResult<IEnumerable<ProductReadAllDTO>>.SuccessResult(productDTOs, "Products retrieved successfully.");
        }

    }
}
