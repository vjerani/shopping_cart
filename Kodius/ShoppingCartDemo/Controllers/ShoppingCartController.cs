using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCartDemo.ApplicationLayer.Dto;
using ShoppingCartDemo.BusinessLayer;
using ShoppingCartDemo.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IShoppingCartManager _shoppingCartManager;

        public ShoppingCartController(ILogger<ShoppingCartController> logger,
                                      IShoppingCartManager shoppingCartManager)
        {
            _logger = logger;
            _shoppingCartManager = shoppingCartManager;
        }

        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout(OrderInput input)
        {
            OrderOutput output = new OrderOutput();
            var items = input.Products.Select(x => new ShoppingItem() { ProductId = x.ProductId, Quantity = x.Quantity });
            var order = await _shoppingCartManager.CheckoutOrderAsync(items, input.PromotionCodes);
            output.Id = order.Id;
            return new JsonResult(output);
        }
        [HttpGet]
        [Route("promotion")]
        public async Task<IActionResult> GetPromotionCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                BadRequest();
            }
            PromotionCodeOutput output = new PromotionCodeOutput();
            var promotion = await _shoppingCartManager.ValidatePromotionCodeAsync(code);
            if (promotion != null)
            {
                output.Valid = true;
                output.Code = new PromotionCodeDto()
                {
                    Discount = promotion.IsDiscount ? promotion.Amount : 0,
                    Offprice = !promotion.IsDiscount ? promotion.Amount : 0,
                    Label = promotion.Name,
                    UseInConjuction = promotion.UseInConjuction
                };
            }
            else
            {
                output.Valid = false;
            }
            return new JsonResult(output);
        }
    }
}
