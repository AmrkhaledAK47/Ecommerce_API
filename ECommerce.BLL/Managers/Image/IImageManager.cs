using ECommerce.Common;

namespace ECommerce.BLL
{
    public interface IImageManager
    {
        Task<GeneralResult<ImageResultDTO>> UploadImageAsync(ImageUploadDTO imageDTO, string basePath, string? schema, string? host);
    }
}