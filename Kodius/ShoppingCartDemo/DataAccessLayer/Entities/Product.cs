using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
