using System.Net.Http.Headers;
using Torc.Assesment.Entities.ViewModel;

namespace TorcAssesment.Test
{
    public class ProductApiTest
    {
        private readonly HttpClient _clientAdminRole;
        private readonly HttpClient _clientClerkRole;
        public ProductApiTest()
        {
            var jwtTokenAdminRole = TestHelpers.GetJwtTokenForAdminRole().Result;
            var jwtTokenClerkRole = TestHelpers.GetJwtTokenForClerkRole().Result;

            _clientAdminRole = TestHelpers._httpClientAdminRole;
            _clientClerkRole = TestHelpers._httpClientClerkRole;

            _clientAdminRole.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtTokenAdminRole);
            _clientClerkRole.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtTokenClerkRole);
        }

        /// <summary>
        /// Extra - Additional test for get method list all products
        /// </summary>
        [Fact]
        public async Task TestGetAllProductsForAdminRole()
        {
            var productModel = new ProductModel { Id = 6, Name = "Test", Price = 50.5m };
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _clientAdminRole.GetAsync("/api/product");

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }


        /// <summary>
        /// Extra - Additional test for get method get one product
        /// </summary>
        [Fact]
        public async Task TestGetProductByIdForAdminRole()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _clientAdminRole.GetAsync("/api/product/4");

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

            var response = await _clientAdminRole.PostAsync("/api/product", TestHelpers.GetJsonStringContent(productModel));

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

            var response = await _clientAdminRole.PutAsync("/api/product", TestHelpers.GetJsonStringContent(productModel));

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }

        /// <summary>
        /// Extra - Additional test delete method delete product
        /// </summary>
        [Fact]
        public async Task TestDeleteProductTest()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _clientAdminRole.DeleteAsync("/api/product/5");

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }

        [Fact]
        public async Task TestCreateProductFailForClerkRole()
        {
            var productModel = new ProductModel { Id = 5, Name = "Test", Price = 50.5m };
            var expectedStatusCode = System.Net.HttpStatusCode.Forbidden;

            var response = await _clientClerkRole.PostAsync("/api/product", TestHelpers.GetJsonStringContent(productModel));

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }

        [Fact]
        public async Task TestDeleteProductFailForClerkRoleTest()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.Forbidden;

            var response = await _clientClerkRole.DeleteAsync("/api/product/5");

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }

        /// <summary>
        /// Extra - Additional test for put method update product
        /// </summary>        
        [Fact]
        public async Task TestUpdateProductFailForClerkRoleTest()
        {
            var productModel = new ProductModel { Id = 4, Name = "Test update", Price = 58.5m };
            var expectedStatusCode = System.Net.HttpStatusCode.Forbidden;

            var response = await _clientClerkRole.PutAsync("/api/product", TestHelpers.GetJsonStringContent(productModel));

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }

        /// <summary>
        /// Extra - Additional test for get method list all products
        /// </summary>
        [Fact]
        public async Task TestGetAllProductsForClerkRole()
        {
            var productModel = new ProductModel { Id = 6, Name = "Test", Price = 50.5m };
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _clientClerkRole.GetAsync("/api/product");

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }


        /// <summary>
        /// Extra - Additional test for get method get one product
        /// </summary>
        [Fact]
        public async Task TestGetProductByIdForClerkRole()
        {
            var expectedStatusCode = System.Net.HttpStatusCode.OK;

            var response = await _clientClerkRole.GetAsync("/api/product/4");

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
        }

    }
}
