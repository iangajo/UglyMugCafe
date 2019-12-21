using System.Collections.Generic;
using Ugly.Mug.Cafe.Domain.Entity;

namespace Ugly.Mug.Cafe.Domain.Request
{
    public interface ICommonOrderRequest
    {
        string Customer { get; set; }
        IEnumerable<ProductRequest> Products { get; set; }
    }
}
