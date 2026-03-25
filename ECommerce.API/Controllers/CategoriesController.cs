using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CategoryDTO>>>> GetAll()
        {
            var categories = await _categoryManager.GetAllCategoriesAsync();
            return ToActionResult(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<CategoryReadDetailsDTO>>> GetById([FromRoute] int id)
        {
            var category = await _categoryManager.GetCategoryByIdAsync(id);
            return ToActionResult(category);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult<CategoryDTO>>> CreateCategory([FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryManager.CreateCategoryAsync(category);
            if (!result.Success)
            {
                return ToActionResult(result);
            }

            if (result.Data == null)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralResult>> UpdateCategory([FromRoute] int id, [FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            category.Id = id;
            var result = await _categoryManager.EditCategoryAsync(category);
            return ToActionResult(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralResult>> DeleteCategory([FromRoute] int id)
        {
            var result = await _categoryManager.DeleteCategoryAsync(id);
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
