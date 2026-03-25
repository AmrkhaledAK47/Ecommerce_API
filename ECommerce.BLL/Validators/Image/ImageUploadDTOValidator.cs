using FluentValidation;

namespace ECommerce.BLL
{
    public class ImageUploadDTOValidator : AbstractValidator<ImageUploadDTO>
    {
        private readonly string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        private const long MaxFileSize = 2 * 1024 * 1024;
        public ImageUploadDTOValidator() 
        {
            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("File is required.")
                .WithErrorCode("ERR-Img-001")
                .WithName("File")

                .Must(file => file.Length > 0)
                .WithMessage("File cannot be empty.")
                .WithErrorCode("ERR-Img-002")
                .WithName("FileSize")

                .Must(file => allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage($"Only the following file types are allowed: {string.Join(", ", allowedExtensions)}.")
                .WithErrorCode("ERR-Img-003")
                .WithName("FileType")

                .Must(file => file.Length <= MaxFileSize)
                .WithMessage($"File size must be less than {MaxFileSize / (1024 * 1024)} MB.")
                .WithErrorCode("ERR-Img-004")
                .WithName("FileSize")

                 .Must(file => file.FileName.Length <= 255)
                 .WithMessage("File name must be less than or equal to 255 characters.")
                 .WithErrorCode("ERR-Img-005")
                 .WithName("FileName");
        }
    }
}
