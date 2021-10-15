using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCartDemo.ApplicationLayer.Dto
{
    public class OrderInput
    {
        [JsonPropertyName("promotionCodes")]
        public IEnumerable<string> PromotionCodes { get; set; }

        [JsonPropertyName("products")]
        public IEnumerable<OrderItemDto> Products { get; set; }
    }

    public class OrderItemDto
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
