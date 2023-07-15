using ContosoUniversity.DAL;
using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    public class UnityOfWork : IDisposable, IUnityOfWork
    {
        private TorcAssesmentContext _dbContext = new TorcAssesmentContext();
        private GenericRepository<Order>? _orderRepository;
        private GenericRepository<Product>? _productRepository;
        private GenericRepository<Customer>? _customerRepository;

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (this._orderRepository == null)
                {
                    this._orderRepository = new GenericRepository<Order>(_dbContext);
                }
                return _orderRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(_dbContext);
                }
                return _productRepository;
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (this._customerRepository == null)
                {
                    this._customerRepository = new GenericRepository<Customer>(_dbContext);
                }
                return _customerRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
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
