using Microsoft.EntityFrameworkCore;
using ShoppingCartDemo.DataAccessLayer;
using ShoppingCartDemo.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.BusinessLayer
{
    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<PromotionCode> _promotionRepository;
        private IEnumerable<PromotionCode> AllPromotionCodes = null;

        public ShoppingCartManager(IBaseRepository<Order> orderRepository,
                                   IBaseRepository<Product> productRepository,
                                   IBaseRepository<PromotionCode> promotionRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _promotionRepository = promotionRepository;
            AllPromotionCodes = _promotionRepository.GetAll();
        }

        public async Task<Order> CheckoutOrderAsync(IEnumerable<ShoppingItem> items, IEnumerable<string> codes)
        {
            ValidatePromotionCodes(codes);
            var order = new Order();
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach(var item in items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                OrderItem orderItem = new OrderItem();
                orderItem.ProductName = product.Name;
                orderItem.ProductPrice = product.Price;
                orderItem.Quantity = item.Quantity;
                orderItems.Add(orderItem);
            }

            order.OrderItems = orderItems;
            var upperCase = codes.Select(x => x.ToUpper()).Distinct();
            var Promotions = AllPromotionCodes.Where(x => upperCase.Contains(x.Name));
            order.Discount = (float) Promotions.Where(x => x.IsDiscount == true).Sum(x => x.Amount);
            order.OffFinalCost = (float)Promotions.Where(x => x.IsDiscount == false).Sum(x => x.Amount);
            order.CalculateTotal();
            await _orderRepository.InsertAndGetIdAsync(order);
            return order;
        }

        public async Task<PromotionCode> ValidatePromotionCodeAsync(string code)
        {
            var promotion = await _promotionRepository.Get().FirstOrDefaultAsync(x => x.Name == code.ToUpper());
            return promotion;
        }

        private void ValidatePromotionCodes(IEnumerable<string> codes)
        {
            if (codes.Count() == 0) return;
            var upperCase = codes.Select(x => x.ToUpper()).Distinct();
            var Promotions = AllPromotionCodes.Where(x => upperCase.Contains(x.Name));
            if (!AllPromotionCodes.Any(x => upperCase.Contains(x.Name)))
            {
                throw new InvalidPromotionCodeException("Invalid or inactive promotion codes are found");
            }
            if (Promotions.Count(x => x.UseInConjuction == false) > 1)
            {
                throw new InvalidPromotionCodeException("Maxiumum one promotion code can be used in conjuction with other codes");
            }
        }
    }
}
