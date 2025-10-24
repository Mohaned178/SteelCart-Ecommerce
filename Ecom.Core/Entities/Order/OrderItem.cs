using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities.Order
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem(int productItemId, string mainImage, string productName, decimal price, int quantity)
        {
            ProductItemId = productItemId;
            MainImage = mainImage;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
        public OrderItem()
        {
            
        }

        public int ProductItemId { get; set; }
        public string MainImage { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
