using ECommerce.DAL;
using FluentValidation;

namespace ECommerce.BLL
{
    public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryDTOValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name is required.")
                .WithErrorCode("ERR-CAT-001")

                .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
                .WithErrorCode("ERR-CAT-002")

                .MaximumLength(50).WithMessage("Category name must not exceed 50 characters.")
                .WithErrorCode("ERR-CAT-003")

                .MustAsync(IsUniqueNameAsync).WithMessage("Category name must be unique.")
                .WithErrorCode("ERR-CAT-004");

        }
        public async Task<bool> IsUniqueNameAsync(string name, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var existingCategory = categories.Any(c => c.Name == name);
            return !existingCategory;
        }
    }
}
