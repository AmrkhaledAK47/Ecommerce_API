using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageManager _imageManager;
        private readonly IWebHostEnvironment _environment;
        public ImageController(IImageManager imageManager, IWebHostEnvironment environment)
        {
            _imageManager = imageManager;
            _environment = environment;
        }
        [HttpPost("upload")]
        public async Task<ActionResult<GeneralResult<ImageResultDTO>>> UploadImage([FromForm] ImageUploadDTO imageUploadDTO)
        {
            var basePath = _environment.ContentRootPath;
            var schema = Request.Scheme;
            var host = Request.Host.Value;

            var result = await _imageManager.UploadImageAsync(imageUploadDTO, basePath, schema, host);
            return ToActionResult(result);
        }
        private ActionResult<GeneralResult<T>> ToActionResult<T>(GeneralResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result);
            }

            if (result.Errors != null && result.Errors.Count > 0)
            {
                return BadRequest(result);
            }

            return NotFound(result);
        }

        private ActionResult<GeneralResult> ToActionResult(GeneralResult result)
        {
            if (result.Success)
            {
                return Ok(result);
            }

            if (result.Errors != null && result.Errors.Count > 0)
            {
                return BadRequest(result);
            }

            return NotFound(result);
        }


    }
}
