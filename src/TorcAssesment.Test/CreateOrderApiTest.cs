using Newtonsoft.Json;
using System.Net.Http.Headers;
using Torc.Assesment.Entities.Models;
using Torc.Assesment.Entities.ViewModel;

namespace TorcAssesment.Test
{
    public class CreateOrderApiTest
    {
        private readonly string _jwtToken = string.Empty;
        private readonly HttpClient _client;
        public CreateOrderApiTest()
        {
            _jwtToken = TestHelpers.GetJwtToken().Result;
            _client = TestHelpers._httpClient;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }

        /// <summary>
        /// This method also works as a integration test of the CreateOrder procedure
        /// </summary>
        [Fact]
        public async void AssertCreateOrderSuccess()
        {
            var createOrderModel = new CreateOrderModel
            {
                ProductId = 1,
                CustomerId = 1,
                Quantity = 3
            };

            var expectedStatusCode = System.Net.HttpStatusCode.Created;

            var response = await _client.PostAsync("/api/createorder", TestHelpers.GetJsonStringContent(createOrderModel));
            var responseValue = JsonConvert.DeserializeObject<OrderCreated>(await response.Content.ReadAsStringAsync());

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
            Assert.IsType<OrderCreated>(responseValue);
            Assert.Equal("Torc Screwdriver", responseValue.ProductName);
            Assert.Equal("Joao Prado", responseValue.CustomerName);
            Assert.Equal(46.50m, responseValue.OrderTotal);
        }


        /// <summary>
        /// Minimum: test error handling for non existent products 
        /// </summary>
        [Fact]
        public async void AssertCreateOrderFailProductNoExists()
        {
            var createOrderModel = new CreateOrderModel
            {
                ProductId = 50,
                CustomerId = 1,
                Quantity = 3
            };

            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var response = await _client.PostAsync("/api/createorder", TestHelpers.GetJsonStringContent(createOrderModel));

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
            var errorMessage = await response.Content.ReadAsStringAsync();
            Assert.True(errorMessage.IndexOf("The product id is required, be greater than zero and exists in the system records.") > 0);
        }


        /// <summary>
        /// Extra - Additional test for customer not exists CreateOrder fail
        /// </summary>
        [Fact]
        public async void AssertCreateOrderFailCustomerNoExists()
        {
            var createOrderModel = new CreateOrderModel
            {
                ProductId = 1,
                CustomerId = 10,
                Quantity = 3
            };

            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var response = await _client.PostAsync("/api/createorder", TestHelpers.GetJsonStringContent(createOrderModel));

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
            var errorMessage = await response.Content.ReadAsStringAsync();
            Assert.True(errorMessage.IndexOf("The customer id is required, be greater than zero and exists in the system records.") > 0);
        }


        /// <summary>
        /// Extra - Additional test for quantity invalid exists CreateOrder fail
        /// </summary>
        [Fact]
        public async void AssertCreateOrderFailQuantityInvalid()
        {
            var createOrderModel = new CreateOrderModel
            {
                ProductId = 1,
                CustomerId = 1,
                Quantity = 0
            };

            var expectedStatusCode = System.Net.HttpStatusCode.BadRequest;

            var response = await _client.PostAsync("/api/createorder", TestHelpers.GetJsonStringContent(createOrderModel));

            TestHelpers.AssertCommonResponseParts(response, expectedStatusCode);
            var errorMessage = await response.Content.ReadAsStringAsync();
            Assert.True(errorMessage.IndexOf("The quantity of products is required and should be greater than zero.") > 0);
        }
    }
}