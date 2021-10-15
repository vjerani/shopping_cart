using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCartDemo.ApplicationLayer.Dto;
using ShoppingCartDemo.DataAccessLayer;
using ShoppingCartDemo.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IBaseRepository<Product> _productRepository;

        public ProductsController(ILogger<ProductsController> logger, IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            ProductsOutput output = new ProductsOutput();
            output.Products = products.Select(x => new ProductDto() { Id = x.Id, Name = x.Name, Price = x.Price });
            return new JsonResult(output);
        }
    }
}
