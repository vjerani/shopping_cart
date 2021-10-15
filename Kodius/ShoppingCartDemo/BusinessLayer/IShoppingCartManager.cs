using ShoppingCartDemo.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartDemo.BusinessLayer
{
    public interface IShoppingCartManager
    {
        Task<Order> CheckoutOrderAsync(IEnumerable<ShoppingItem> items, IEnumerable<string> codes);
        Task<PromotionCode> ValidatePromotionCodeAsync(string code);
    }
}