using Microsoft.EntityFrameworkCore;
using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    internal class ProductRepository : IProductRepository, IDisposable
    {
        private readonly TorcAssesmentContext _dbContext;

        public ProductRepository()
        {
            _dbContext = new TorcAssesmentContext();
        }

        public ProductRepository(TorcAssesmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Product.FindAsync(id);
        }

        public async Task InsertAsync(Product Product)
        {
            await _dbContext.Product.AddAsync(Product);
        }

        public void UpdateAsync(Product Product)
        {
            _dbContext.Entry(Product).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var productToDelete = await GetProductByIdAsync(id);
            if (productToDelete != null)
            {
                _dbContext.Product.Remove(productToDelete);
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
