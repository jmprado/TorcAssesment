using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateUser(string username, string password);
    }
}
