using Ecom.Core.Entities;
using Ecom.Core.Entities.Order;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _context = context;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentAsync(string basketId, int? deliveryId)
        {
            var basket = await _unitOfWork.CustomerBasket.GetBasketAsync(basketId);
            StripeConfiguration.ApiKey = _configuration["StripeSetting:secretKey"];
            decimal shippingPrice = 0m;
            if (deliveryId.HasValue)
            {
                var delivery = await _context.DeliveryMethods.AsNoTracking().FirstOrDefaultAsync(m => m.Id == deliveryId.Value);
                shippingPrice = deliveryId.Value;
            }
            foreach (var item in basket.basketItems)
            {
                var product = await _unitOfWork.productRepositry.GetByIdAsync(item.Id);
                item.Price = product.NewPrice;
            }
            PaymentIntentService paymentIntentService = new();
            PaymentIntent _intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.basketItems.Sum(m => m.Qunatity * (m.Price * 100)) + (long)(shippingPrice * 100),

                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                _intent = await paymentIntentService.CreateAsync(option);
                basket.PaymentIntentId = _intent.Id;
                basket.ClientSecret = _intent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.basketItems.Sum(m => m.Qunatity * (m.Price * 100)) + (long)(shippingPrice * 100),

                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, option);
            }
            await _unitOfWork.CustomerBasket.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Orders> UpdateOrderFaild(string PaymentInten)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.PaymentIntentId == PaymentInten);
            if (order == null)
            {
                return null;
            }
            order.status = Status.PaymentFailed;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Orders> UpdateOrderSuccess(string PaymentInten)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.PaymentIntentId == PaymentInten);
            if (order == null)
            {
                return null;
            }
            order.status = Status.PaymentReceived;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
