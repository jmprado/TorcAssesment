using Microsoft.AspNetCore.Mvc;
using Torc.Assesment.Dal;
using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;

        public ProductController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize = 10, int page = 1, string? sortBy = "")
        {
            var listProducts = await _unityOfWork.ProductRepository.GetAsync();

            if (!string.IsNullOrEmpty(sortBy))
                listProducts = listProducts.OrderBy(c => c.GetType().GetProperty(sortBy)?.GetValue(c)).ToList();

            listProducts = listProducts.Skip(page).Take(pageSize).ToList();

            return Ok(listProducts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _unityOfWork.ProductRepository.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert(Product product)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            await _unityOfWork.ProductRepository.InsertAsync(product);
            await _unityOfWork.SaveChangesAsync();

            return StatusCode(201, "Product sucessfuly created");
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Product product)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(ModelState);

            _unityOfWork.ProductRepository.Update(product);
            await _unityOfWork.SaveChangesAsync();

            return StatusCode(201, "Product sucessfuly updated");
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unityOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
                return new BadRequestObjectResult($"Unable to delete product id {id}, product not found");

            _unityOfWork.ProductRepository.Delete(product);

            return Ok($"Product id {id} deleted");
        }
    }
}
