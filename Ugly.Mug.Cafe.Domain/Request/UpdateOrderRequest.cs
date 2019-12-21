using System;
using System.Collections.Generic;

namespace Ugly.Mug.Cafe.Domain.Request
{
    public class UpdateOrderRequest : ICommonOrderRequest
    {
        public Guid OrderNumber { get; set; }
        public string Customer { get; set; }
        public IEnumerable<ProductRequest> Products { get; set; }
    }
}
