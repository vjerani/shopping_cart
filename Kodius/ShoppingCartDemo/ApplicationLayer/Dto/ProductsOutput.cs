using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCartDemo.ApplicationLayer.Dto
{
    public class ProductsOutput
    {
        [JsonPropertyName("products")]
        public IEnumerable<ProductDto> Products { get; set; }
    }

    public class ProductDto
    {
        [JsonPropertyName("productId")]
        public int Id { get; set; }

        [JsonPropertyName("productName")]
        public string Name { get; set; }

        [JsonPropertyName("productPrice")]
        public float Price { get; set; }
    }
}
