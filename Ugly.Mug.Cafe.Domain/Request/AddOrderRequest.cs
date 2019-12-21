using System.Collections.Generic;
using Ugly.Mug.Cafe.Domain.Entity;

namespace Ugly.Mug.Cafe.Domain.Request
{
    public class AddOrderRequest : ICommonOrderRequest
    {
        public string Customer { get; set; }
        public IEnumerable<ProductRequest> Products { get; set; }
    }
}
