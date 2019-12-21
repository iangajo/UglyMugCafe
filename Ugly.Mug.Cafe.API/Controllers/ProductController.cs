using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Ugly.Mug.Cafe.Core.Products;

namespace Ugly.Mug.Cafe.API.Controllers
{
    [ApiController]
    public class ProductController : UglyMugCafeBaseController
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("v1/product/list")]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productRepository.GetProducts();

            return Ok(response);
        }
    }
}