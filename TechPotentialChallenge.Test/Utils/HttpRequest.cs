using System.Text;
using TechPotentialChallenge.Communication.Request;
using TechPotentialChallenge.Infrastructure;

namespace TechPotentialChallenge.Test.Utils
{
    public class HttpRequest
    {
        private static readonly HttpClient _client = new();
        private static readonly string _url = Config.HttpPath;

        public HttpRequest()
        {
            _client.BaseAddress = new Uri(_url);
        }

        public static async Task<HttpResponseMessage> CreateOrderAsync(RequestSaleJson requestSaleJson)
        {
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(requestSaleJson);
            var request = new HttpRequestMessage(HttpMethod.Post, _url)
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            return await _client.SendAsync(request);
        }
        public static async Task<HttpResponseMessage> GetOrderAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_url}/{id}");
            return await _client.SendAsync(request);
        }
        public static async Task<HttpResponseMessage> UpdateOrderAsync(Guid id, UpdateStatusSaleJson updateStatusSaleJson)
        {
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(updateStatusSaleJson);
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_url}/{id}")
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            return await _client.SendAsync(request);
        }
    }
}
