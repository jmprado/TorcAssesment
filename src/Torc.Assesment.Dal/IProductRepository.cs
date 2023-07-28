using Torc.Assesment.Entities.Models;
using Torc.Assesment.Entities.ViewModel;

namespace Torc.Assesment.Dal
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> InsertAsync(ProductModel product);
        Task<Product> UpdateAsync(ProductModel product);
        Task DeleteAsync(int id);
    }
}
