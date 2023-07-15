using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task InsertAsync(Customer customer);
        void UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
