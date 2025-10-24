using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class CustomerBasketRepositry : ICustomerBasketRepositry
    {
        private readonly StackExchange.Redis.IDatabase _database;

        public CustomerBasketRepositry(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public Task<bool> DeleteBasketAsync(string id)
        {
            return _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var res = await _database.StringGetAsync(id);
            if (!string.IsNullOrEmpty(res))
            {
                return JsonSerializer.Deserialize<CustomerBasket>(res);
            }
            return null;
        }
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var _basket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(3));
            if (_basket)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }
    }
}
