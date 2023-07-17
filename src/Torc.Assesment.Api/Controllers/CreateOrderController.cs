using Microsoft.AspNetCore.Mvc;
using Torc.Assesment.Entities.ViewModel;
using TorcAssesment.BusinessLogic;

namespace Torc.Assesment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateOrderController : ControllerBase
    {
        private readonly IOrders _orders;

        public CreateOrderController(IOrders orders)
        {
            _orders = orders;
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="createOrderModel"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(CreateOrderModel createOrderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderCreated = await _orders.CreateOrder(createOrderModel);
            return StatusCode(201, orderCreated);
        }
    }
}
