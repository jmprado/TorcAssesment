using AutoMapper;
using Torc.Assesment.Entities.Models;

namespace Torc.Assesment.Dal
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly IUnityOfWork _unityOfWork;


        public UserRepository(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<User?> AuthenticateUser(string username, string password)
        {
            var findUser = await _unityOfWork.UserRepository.GetAsync(user => user.Username == username && user.Password == password);
            return findUser.FirstOrDefault();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _unityOfWork.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
