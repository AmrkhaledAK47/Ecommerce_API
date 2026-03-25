using ECommerce.Common;
using FluentValidation;

namespace ECommerce.BLL
{
    public class ImageManager : IImageManager
    {
        private readonly IValidator<ImageUploadDTO> _imageUploadValidator;
        private readonly IErrorMapper _errorMapper;
        public ImageManager(IValidator<ImageUploadDTO> imageUploadValidator, IErrorMapper errorMapper)
        {
            _imageUploadValidator = imageUploadValidator;
            _errorMapper = errorMapper;
        }
        public async Task<GeneralResult<ImageResultDTO>> UploadImageAsync
            (ImageUploadDTO imageDTO,
             string basePath,
             string? schema,
             string? host)
        {
            if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(host))
            {
                return GeneralResult<ImageResultDTO>.FailureResult("Invalid schema or host");
            }

            var validationResult = await _imageUploadValidator.ValidateAsync(imageDTO);
            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapToErrors(validationResult);
                return GeneralResult<ImageResultDTO>.FailureResult(errors);
            }

            var file = imageDTO.File;
            var extension = Path.GetExtension(file.FileName).ToLower();
            var cleanFileName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "-").ToLower();
            var uniqueFileName = $"{cleanFileName}-{Guid.NewGuid()}{extension}";
            var directoryPath = Path.Combine(basePath, "Files");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var filePath = Path.Combine(directoryPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var url = $"{schema}://{host}/Files/{uniqueFileName}";
            var imageResult = new ImageResultDTO(url);
            return GeneralResult<ImageResultDTO>.SuccessResult(imageResult);
        }
    }
}
