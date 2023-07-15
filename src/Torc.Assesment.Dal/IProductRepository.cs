using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task InsertAsync(Product product);
        void UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
