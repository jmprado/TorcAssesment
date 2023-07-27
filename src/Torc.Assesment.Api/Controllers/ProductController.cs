using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Torc.Assesment.Dal;
using Torc.Assesment.Entities.ViewModel;

namespace Torc.Assesment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize = 10, int page = 1, string? sortBy = "")
        {
            var listProducts = await _productRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(sortBy))
                listProducts = listProducts.OrderBy(c => c.GetType().GetProperty(sortBy)?.GetValue(c)).ToList();

            listProducts = listProducts.Skip(page).Take(pageSize).ToList();

            return Ok(listProducts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert(ProductModel product)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            await _productRepository.InsertAsync(product);

            return StatusCode(201, "Product sucessfuly created");
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(ProductModel product)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            await _productRepository.UpdateAsync(product);

            return StatusCode(200, "Product sucessfuly updated");
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            return Ok($"Product id {id} deleted");
        }
    }
}
