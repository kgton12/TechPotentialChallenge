using Bogus;
using Bogus.Extensions.Brazil;
using Newtonsoft.Json;
using System.Net;
using TechPotentialChallenge.Communication.Request;
using TechPotentialChallenge.Domain.Model;
using TechPotentialChallenge.Test.Utils;

namespace TechPotentialChallenge.Test
{
    public class OrderTest
    {
        private Guid TestOrderId { get; set; }

        [Fact]
        public async Task ExecuteAllTests()
        {
            await OrderPostTest();
            await OrderGetTest();
            await UpdateOrderAsync();
        }

        internal async Task OrderPostTest()
        {
            var orderItemRequestFaker = new Faker<OrderItemRequest>()
                .StrictMode(true)
                .RuleFor(r => r.Product, f => f.Commerce.ProductName())
                .RuleFor(r => r.Quantity, f => f.Random.Number(1, 10))
                .RuleFor(r => r.Price, f => f.Random.Decimal(1, 1000));

            var requestSaleData = new Faker<RequestSaleJson>()
                .StrictMode(true)
                .RuleFor(r => r.NameSaler, f => f.Person.FullName)
                .RuleFor(r => r.CPFSaler, f => f.Person.Cpf(false))
                .RuleFor(r => r.EmailSaler, f => f.Internet.Email())
                .RuleFor(r => r.TelephoneSeler, f => f.Phone.PhoneNumber())
                .RuleFor(r => r.OrderItem, f => [.. orderItemRequestFaker.Generate(new Random().Next(1, 11))])
                .Generate();

            var response = await HttpRequest.CreateOrderAsync(requestSaleData);

            var order = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());

            TestOrderId = order?.Id ?? Guid.Empty;

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.IsType<Order>(order);
            Assert.True(response.IsSuccessStatusCode);
        }

        internal async Task OrderGetTest()
        {
            var response = await HttpRequest.GetOrderAsync(TestOrderId);

            var order = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
            Assert.IsType<Order>(order);
        }

        internal async Task UpdateOrderAsync()
        {
            var updateStatusSaleJson = new UpdateStatusSaleJson
            {
                Status = Domain.Enuns.Status.PaymentApproved
            };

            var response = await HttpRequest.UpdateOrderAsync(TestOrderId, updateStatusSaleJson);

            var order = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
            Assert.IsType<Order>(order);
        }
    }
}
