using Torc.Assesment.Dal;
using Torc.Assesment.Entities.Models;
using Torc.Assesment.Entities.ViewModel;

namespace TorcAssesment.BusinessLogic
{
    /// <summary>
    /// Sample for using a business logic layer to isolate the database access from the 
    /// view layer
    /// </summary>
    public class Orders : IOrders, IDisposable
    {
        private readonly IOrderRepository _orderRepository;

        public Orders(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderCreated> CreateOrder(CreateOrderModel createOrderModel)
        {
            var orderCreated = await _orderRepository.ExecCreateOrderProcedure(createOrderModel);
            return orderCreated;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _orderRepository.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}