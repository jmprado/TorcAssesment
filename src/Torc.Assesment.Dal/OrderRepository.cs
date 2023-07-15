using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private readonly TorcAssesmentContext _dbContext;

        public OrderRepository()
        {
            _dbContext = new TorcAssesmentContext();
        }

        public OrderRepository(TorcAssesmentContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task ExecCreateOrderProcedure(CreateOrderModel createOrderModel)
        {
            await _dbContext.Procedures.CreateOrderAsync(createOrderModel.ProductId, createOrderModel.CustomerId, createOrderModel.Quantity);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _dbContext.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
