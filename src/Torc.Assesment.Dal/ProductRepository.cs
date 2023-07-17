using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Torc.Assesment.Entities.Models;
using Torc.Assesment.Entities.ViewModel;

namespace Torc.Assesment.Dal
{
    internal class ProductRepository : IProductRepository, IDisposable
    {
        private readonly IUnityOfWork _unityOfWork;

        public ProductRepository()
        {
            _unityOfWork = new UnityOfWork();
        }

        public ProductRepository(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unityOfWork.ProductRepository.GetAsync(); ;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _unityOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(ProductModel product)
        {
            await _unityOfWork.ProductRepository.InsertAsync(product);
            await _unityOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductModel product)
        {
            _unityOfWork.ProductRepository.Update(product);
            await _unityOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unityOfWork.ProductRepository.DeleteAsync(id);
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
