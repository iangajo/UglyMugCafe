using System.Collections.Generic;
using System.Threading.Tasks;
using Ugly.Mug.Cafe.Domain.Response;

namespace Ugly.Mug.Cafe.Core.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductResponse>> GetProducts();
    }
}
