using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ugly.Mug.Cafe.Data;
using Ugly.Mug.Cafe.Domain.Response;

namespace Ugly.Mug.Cafe.Core.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductResponse>> GetProducts()
        {
            var products = await _dbContext.Products.AsNoTracking().ToListAsync();

            if (!products.Any()) return new List<ProductResponse>();

            return products.Select(p => new ProductResponse()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Quantity = 0

            }).ToList();
        }
    }
}
