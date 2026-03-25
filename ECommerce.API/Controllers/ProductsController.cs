using ECommerce.BLL;
using ECommerce.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<ProductReadAllDTO>>>> GetAll()
        {
            var products = await _productManager.GetAllProductsAsync();
            return ToActionResult(products);
        }
        [HttpGet("pagination")]
        public async Task<ActionResult<GeneralResult<PagedResult<ProductReadAllDTO>>>> GetAllPaged([FromQuery] PaginationParameters? paginationParameters = null, [FromQuery] ProductFilterParameters? filterParameters = null)
        {
            var pagedProducts = await _productManager.GetAllPagedProductsAsync(paginationParameters, filterParameters);
            return ToActionResult(pagedProducts);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<ProductReadDetailsDTO>>> GetById([FromRoute] int id)
        {
            var product = await _productManager.GetProductByIdAsync(id);
            return ToActionResult(product);
        }
        [HttpGet("search")]
        public async Task<ActionResult<GeneralResult<IEnumerable<ProductReadAllDTO>>>> SearchByName([FromQuery] string name)
        {
            var productsResult = await _productManager.SearchProductsByNameAsync(name);
            return ToActionResult(productsResult);
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResult<ProductReadDetailsDTO>>> CreateProduct([FromBody] ProductCreateDTO productCreateDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var createdProduct = await _productManager.CreateProductAsync(productCreateDTO);
            if (!createdProduct.Success)
            {
                return ToActionResult(createdProduct);
            }

            if (createdProduct.Data == null)
            {
                return BadRequest(createdProduct);
            }

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Data.Id }, createdProduct);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralResult>> UpdateProduct([FromRoute] int id, [FromBody] ProductEditDTO productUpdateDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            productUpdateDTO.Id = id;
            var result = await _productManager.EditProductAsync(productUpdateDTO);
            return ToActionResult(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralResult>> DeleteProduct([FromRoute] int id)
        {
            var result = await _productManager.DeleteProductAsync(id);
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
