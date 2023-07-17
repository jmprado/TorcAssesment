using Torc.Assesment.Dal;
using Torc.Assesment.Entities.Models;

namespace TorcAssesment.BusinessLogic
{
    public class Orders : IOrders
    {
        private readonly IOrderRepository _orderRepository;

        public Orders(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task CreateOrder(CreateOrderModel createOrderModel)
        {
            await _orderRepository.ExecCreateOrderProcedure(createOrderModel);
        }
    }
}