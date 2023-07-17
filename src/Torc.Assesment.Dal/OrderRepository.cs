using Torc.Assesment.Entities.Models;
using Torc.Assesment.Entities.ViewModel;

namespace Torc.Assesment.Dal
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private readonly TorcAssesmentContext _dbContext;
        private readonly IUnityOfWork _unityOfWork;

        public OrderRepository()
        {
            _dbContext = new TorcAssesmentContext();
            _unityOfWork = new UnityOfWork();
        }

        public OrderRepository(TorcAssesmentContext dbContext, IUnityOfWork unityOfWork)
        {
            _dbContext = dbContext;
            _unityOfWork = unityOfWork;
        }


        public async Task<OrderCreated> ExecCreateOrderProcedure(CreateOrderModel createOrderModel)
        {
            return await _dbContext.Procedures.CreateOrderAsync(createOrderModel.ProductId, createOrderModel.CustomerId, createOrderModel.Quantity);
        }

        public async Task<IEnumerable<Order>> ListOrders()
        {
            return await _unityOfWork.OrderRepository.GetAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                {
                    _dbContext.Dispose();
                    _unityOfWork.Dispose(); ;
                }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
