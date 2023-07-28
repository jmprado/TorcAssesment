using EntityFrameworkCore.Testing.Moq;
using Microsoft.EntityFrameworkCore;
using Torc.Assesment.Entities.Models;

namespace TorcAssesment.Test
{
    /// <summary>
    /// Extras - Mock DbContext. Test database operations.
    /// </summary>
    public class ProductRepoTest
    {
        private readonly TorcAssesmentContext _mockedDbContext;

        public ProductRepoTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<TorcAssesmentContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _mockedDbContext = Create.MockedDbContextFor<TorcAssesmentContext>(dbContextOptions);
        }


        private readonly Product[] _products =
        {
            new Product{
                Id = 1,
                Name = "Test",
                Price = 1.0m
            },

            new Product{
                Id = 2,
                Name = "Test 2",
                Price = 2.0m
            },

            new Product{
                Id = 3,
                Name = "Test 3",
                Price = 3.0m
            },
        };


        [Fact]
        public void Test_Create_Product()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Test",
                Price = 1.0m
            };

            _mockedDbContext.Set<Product>().Add(product);
            _mockedDbContext.SaveChanges();

            Assert.Equal(product, _mockedDbContext.Find<Product>(product.Id));
            Assert.Equal(product.Name, _mockedDbContext.Find<Product>(product.Id)?.Name);
            Assert.Equal(product.Price, _mockedDbContext.Find<Product>(product.Id)?.Price);
        }


        [Fact]
        public void Test_Update_Product()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Test",
                Price = 1.0m
            };

            _mockedDbContext.Set<Product>().Add(product);
            _mockedDbContext.SaveChanges();

            var newProductName = "Test updated";
            product.Name = newProductName;

            _mockedDbContext.Set<Product>().Update(product);
            _mockedDbContext.SaveChanges();

            Assert.Equal(product, _mockedDbContext.Find<Product>(product.Id));
            Assert.Equal(newProductName, _mockedDbContext.Find<Product>(product.Id)?.Name);
            Assert.Equal(product.Price, _mockedDbContext.Find<Product>(product.Id)?.Price);
        }


        [Fact]
        public void Test_GetAll_Product()
        {
            foreach (var product in _products)
                _mockedDbContext.Set<Product>().Add(product);

            _mockedDbContext.SaveChanges();

            var allProducts = _mockedDbContext.Product.ToList();

            Assert.Equal(3, allProducts.Count);
        }


        [Fact]
        public void Test_GetById_Product()
        {
            foreach (var product in _products)
                _mockedDbContext.Set<Product>().Add(product);

            _mockedDbContext.SaveChanges();

            var product3 = _mockedDbContext.Product.Where(p => p.Id == 3).FirstOrDefault();

            Assert.Equal("Test 3", product3?.Name);
        }


        [Fact]
        public void Test_Delete_Product()
        {
            foreach (var product in _products)
                _mockedDbContext.Set<Product>().Add(product);

            _mockedDbContext.SaveChanges();

            var product2 = _mockedDbContext.Product.Where(p => p.Id == 2).FirstOrDefault();
            _mockedDbContext.Product.Remove(product2);
            _mockedDbContext.SaveChanges();

            var allProducts = _mockedDbContext.Product.ToList();

            Assert.Equal(2, allProducts.Count);
        }
    }
}
