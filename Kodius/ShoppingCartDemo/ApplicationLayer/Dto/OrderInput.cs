using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCartDemo.ApplicationLayer.Dto
{
    public class OrderInput
    {
        [JsonPropertyName("promotionCodes")]
        public IEnumerable<string> PromotionCodes { get; set; }

        [Required]
        [JsonPropertyName("products")]
        public IEnumerable<OrderItemDto> Products { get; set; }
    }

    public class OrderItemDto
    {
        [Required]
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [Required]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
