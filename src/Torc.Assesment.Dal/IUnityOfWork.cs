using Torc.Assesment.Entities.Models;
using TorcAssesment.Dal;

namespace Torc.Assesment.Dal
{
    public interface IUnityOfWork
    {
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<Order> OrderRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<User> UserRepository { get; }

        Task SaveChangesAsync();
        void Dispose();

    }
}