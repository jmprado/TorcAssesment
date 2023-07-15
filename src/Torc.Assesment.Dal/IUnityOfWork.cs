using ContosoUniversity.DAL;
using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    public interface IUnityOfWork
    {
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<Order> OrderRepository { get; }
        GenericRepository<Product> ProductRepository { get; }

        Task SaveChangesAsync();
        void Dispose();

    }
}