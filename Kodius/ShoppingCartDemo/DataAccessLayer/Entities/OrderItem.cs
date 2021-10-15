using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer.Entities
{
    public class OrderItem: BaseEntity
    {
        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }

        public float ProductPrice { get; set; }
    }
}
