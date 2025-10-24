using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities.Order
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }

        public DeliveryMethod()
        {

        }
        public DeliveryMethod(string name, decimal price, string deliveryTime, string description)
        {
            Name = name;
            Price = price;
            DeliveryTime = deliveryTime;
            Description = description;
        }


    }
}
