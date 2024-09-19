using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechPotentialChallenge.API.Common;
using TechPotentialChallenge.Communication.Request;
using TechPotentialChallenge.Domain.Enuns;
using TechPotentialChallenge.Domain.Model;
using TechPotentialChallenge.Domain.Validate;
using TechPotentialChallenge.Infrastructure;

namespace TechPotentialChallenge.API.Endpoint.Sales
{
    public class SaleEndpoint : IEndpoint
    {
        private static TechPotentialChallengeDbContext _dbContext = null!;

        public static void Map(IEndpointRouteBuilder app)
        {
            _dbContext ??= new();

            app.MapPost("/", CreateOrderAsync);
            app.MapGet("/{id}", GetOrderAsync);
            app.MapPut("/{id}", UpdateOrderAsync);
        }

        public static async Task<IResult> CreateOrderAsync(RequestSaleJson requestSaleJson)
        {
            var idSale = Guid.NewGuid();

            var order = new Order
            {
                Id = idSale,
                NameSaler = requestSaleJson.NameSaler,
                OrderItem = requestSaleJson.OrderItem.Select(item => new OrderItem
                {
                    OrderId = idSale,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            var validator = new OrderValidator();
            var results = validator.Validate(order);

            if (results.IsValid is false)
                return Results.BadRequest(results.Errors.Select(e => e.ErrorMessage).ToList());

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return Results.Created(string.Empty, order);
        }

        public static async Task<IResult> GetOrderAsync(Guid id)
        {
            var order = await _dbContext.Orders
                .Include(x => x.OrderItem)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order is null)
                return Results.NotFound();

            return Results.Ok(order);
        }

        public static async Task<IResult> UpdateOrderAsync([FromRoute] Guid id, [FromBody] UpdateStatusSaleJson updateStatusSaleJson)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order is null)
                return Results.NotFound();

            if (order.Status == Status.AwaitingPayment)
            {
                if (updateStatusSaleJson.Status == Status.PaymentApproved || updateStatusSaleJson.Status == Status.Canceled)
                {
                    order.Status = updateStatusSaleJson.Status;
                }
            }
            else if (order.Status == Status.PaymentApproved)
            {
                if (updateStatusSaleJson.Status == Status.SentToCarrier || updateStatusSaleJson.Status == Status.Canceled)
                {
                    order.Status = updateStatusSaleJson.Status;
                }
            }
            else if (order.Status == Status.SentToCarrier && updateStatusSaleJson.Status == Status.Delivered)
            {
                order.Status = updateStatusSaleJson.Status;
            }

            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
            return Results.Ok(order);
        }
    }
}
