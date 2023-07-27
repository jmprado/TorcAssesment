using System.Net.Http.Headers;
using Torc.Assesment.Entities.ViewModel;

namespace TorcAssesment.Test
{
    public class ProductApiTest
    {
        private readonly string _jwtToken = string.Empty;
        private readonly HttpClient _client;
        public ProductApiTest()
        {
            _jwtToken = TestHelpers.GetJwtToken().Result;
            _client = TestHelpers._httpClient;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }

        /// <summary>
        /// Extra - Additional test for get method list all products
        /// </summary>
        [Fact]
        public async Task TestGetAllProducts()
        {
            var productModel = new ProductModel { Id = 6, Name = "Test", Price = 50.5m };
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _client.GetAsync("/api/product");

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }


        /// <summary>
        /// Extra - Additional test for get method get one product
        /// </summary>
        [Fact]
        public async Task TestGetProductById()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _client.GetAsync("/api/product/4");

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }


        /// <summary>
        /// Minimum - Test post method CreateProduct
        /// </summary>
        [Fact]
        public async Task TestCreateProductTest()
        {
            var productModel = new ProductModel { Id = 5, Name = "Test", Price = 50.5m };
            var expectedStatusCode = System.Net.HttpStatusCode.Created;

            var response = await _client.PostAsync("/api/product", TestHelpers.GetJsonStringContent(productModel));

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }


        /// <summary>
        /// Extra - Additional test for put method update product
        /// </summary>        
        [Fact]
        public async Task TestUpdateProductTest()
        {
            var productModel = new ProductModel { Id = 4, Name = "Test update", Price = 58.5m };
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _client.PutAsync("/api/product", TestHelpers.GetJsonStringContent(productModel));

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }

        /// <summary>
        /// Extra - Additional test delete method delete product
        /// </summary>
        [Fact]
        public async Task TestDeleteProductTest()
        {
            var productModel = new ProductModel { Id = 5, Name = "Test update", Price = 58.5m };
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _client.DeleteAsync("/api/product/5");

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }
    }
}
