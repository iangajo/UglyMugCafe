using System;
using System.Collections.Generic;
using Ugly.Mug.Cafe.Domain.Enum;

namespace Ugly.Mug.Cafe.Domain.Response
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public string Customer { get; set; }
        public Guid OrderNumber { get; set; }
        public IEnumerable<ProductResponse>  Orders { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
