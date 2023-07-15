using Microsoft.AspNetCore.Mvc;
using Torc.Assesment.Dal;
using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateOrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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

            await _orderRepository.ExecCreateOrderProcedure(createOrderModel);
            return StatusCode(201, "Order sucessfuly created");
        }
    }
}
