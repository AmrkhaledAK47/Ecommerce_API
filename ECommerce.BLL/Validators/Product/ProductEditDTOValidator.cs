using ECommerce.DAL;
using FluentValidation;

namespace ECommerce.BLL
{
    public class ProductEditDTOValidator : AbstractValidator<ProductEditDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductEditDTOValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("Product id is required.")
                .WithErrorCode("ERR-PROD-000");

            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Product title is required.")
                .WithErrorCode("ERR-PROD-001")

                .MinimumLength(3)
                .WithMessage("Product title must be at least 3 characters long.")
                .WithErrorCode("ERR-PROD-002")

                .MaximumLength(100)
                .WithMessage("Product title must not exceed 100 characters.")
                .WithErrorCode("ERR-PROD-003")

                .MustAsync(IsUniqueTitleAsync)
                .WithMessage("Product title must be unique.")
                .WithErrorCode("ERR-PROD-004");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("Product description is required.")
                .WithErrorCode("ERR-PROD-005")

                .MaximumLength(500)
                .WithMessage("Product description must not exceed 500 characters.")
                .WithErrorCode("ERR-PROD-006");

            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("Product price is required.")
                .WithErrorCode("ERR-PROD-007")

                .InclusiveBetween(1, 5000)
                .WithMessage("Product price must be between 1 and 5000.")
                .WithErrorCode("ERR-PROD-008");

            RuleFor(p => p.Count)
                .NotEmpty()
                .WithMessage("Product count is required.")
                .WithErrorCode("ERR-PROD-009")

                .GreaterThan(0)
                .WithMessage("Product count must be greater than 0.")
                .WithErrorCode("ERR-PROD-010");

            RuleFor(p => p.ExpiryDate)
                .NotEmpty()
                .WithMessage("Product expiry date is required.")
                .WithErrorCode("ERR-PROD-011")

                .GreaterThan(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Product expiry date must be in the future.")
                .WithErrorCode("ERR-PROD-012")

                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(5)))
                .WithMessage("Product expiry date must be within 5 years.")
                .WithErrorCode("ERR-PROD-013");

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .WithMessage("Product category is required.")
                .WithErrorCode("ERR-PROD-014")

                .MustAsync(async (categoryId, cancellationToken) =>
                {
                    var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                    return category != null;
                })
                .WithMessage("Product category must exist.")
                .WithErrorCode("ERR-PROD-015");

            RuleFor(p => p.CurrentImageUrl)
                .MaximumLength(200)
                .WithMessage("Product image URL must not exceed 200 characters.")
                .WithErrorCode("ERR-PROD-016")

                .When(p => !string.IsNullOrEmpty(p.CurrentImageUrl));
        }

        public async Task<bool> IsUniqueTitleAsync(ProductEditDTO product, string title, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            var existingProduct = products.Any(p => p.Title == title && p.Id != product.Id);
            return !existingProduct;
        }
    }
}
