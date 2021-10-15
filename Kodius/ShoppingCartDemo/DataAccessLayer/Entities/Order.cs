using ShoppingCartDemo.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer.Entities
{
    public class Order: BaseEntity
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public float Discount { get; set; }
        public float OffFinalCost { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public float OrderTotal { get; set; }
        public void CalculateTotal()
        {
            OrderTotal = (float)OrderItems.Sum(x => Math.Round(x.ProductPrice * x.Quantity, 2));
            OrderTotal = (float)(OrderTotal - Math.Round(OrderTotal * Discount, 2));
            OrderTotal = OrderTotal - OffFinalCost;
        }
    }
}
