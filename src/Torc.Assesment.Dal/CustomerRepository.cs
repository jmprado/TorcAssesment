using Microsoft.EntityFrameworkCore;
using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    internal class CustomerRepository : ICustomerRepository, IDisposable
    {
        private readonly TorcAssesmentContext _dbContext;

        public CustomerRepository()
        {
            _dbContext = new TorcAssesmentContext();
        }

        public CustomerRepository(TorcAssesmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbContext.Customer.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _dbContext.Customer.FindAsync(id);
        }

        public async Task InsertAsync(Customer Customer)
        {
            await _dbContext.Customer.AddAsync(Customer);
        }

        public void UpdateAsync(Customer Customer)
        {
            _dbContext.Entry(Customer).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var customerToDelete = await GetCustomerByIdAsync(id);
            if (customerToDelete != null)
            {
                _dbContext.Customer.Remove(customerToDelete);
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
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
