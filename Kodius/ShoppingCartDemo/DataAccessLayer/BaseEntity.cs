using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer
{
    public abstract class BaseEntity : IBaseEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
