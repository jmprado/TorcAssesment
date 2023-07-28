using AutoMapper;
using Torc.Assesment.Entities.Models;
using Torc.Assesment.Entities.ViewModel;

namespace Torc.Assesment.Dal
{
    /// <summary>
    /// Product repository using unity of work pattern
    /// </summary>
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;


        public ProductRepository(IUnityOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unityOfWork.ProductRepository.GetAsync(); ;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _unityOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<Product> InsertAsync(ProductModel product)
        {
            var productInsert = _mapper.Map<Product>(product);
            await _unityOfWork.ProductRepository.InsertAsync(productInsert);
            await _unityOfWork.SaveChangesAsync();
            return productInsert;
        }

        public async Task<Product> UpdateAsync(ProductModel product)
        {
            var productUpdate = _mapper.Map<Product>(product);

            _unityOfWork.ProductRepository.Update(productUpdate);
            await _unityOfWork.SaveChangesAsync();

            return productUpdate;
        }

        public async Task DeleteAsync(int id)
        {
            await _unityOfWork.ProductRepository.DeleteAsync(id);
            await _unityOfWork.SaveChangesAsync();
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
