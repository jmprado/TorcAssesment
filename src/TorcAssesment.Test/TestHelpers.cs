using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Torc.Assesment.Api.Model;

namespace TorcAssesment.Test
{
    public static class TestHelpers
    {
        public static readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7210") };
        private const string _jsonMediaType = "application/json";
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        public static async Task<string> GetJwtToken()
        {
            var user = new User() { Username = "joaoprado", Password = "joaopassword" };
            var response = await _httpClient.PostAsync("/api/security", TestHelpers.GetJsonStringContent(user));

            return await response.Content.ReadAsStringAsync();
        }


        public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
                HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
                T expectedContent)
        {
            AssertCommonResponseParts(response, expectedStatusCode);
            Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
            Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
                await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions));
        }

        public static void AssertCommonResponseParts(
            HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        public static StringContent GetJsonStringContent<T>(T model)
            => new(JsonSerializer.Serialize(model), Encoding.UTF8, _jsonMediaType);
    }
}
