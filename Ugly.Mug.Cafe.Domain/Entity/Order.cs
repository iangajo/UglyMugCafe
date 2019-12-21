using System;
using Ugly.Mug.Cafe.Domain.Enum;

namespace Ugly.Mug.Cafe.Domain.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Customer { get; set; }
        public Guid OrderNumber { get; set; }
        public string Request { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = OrderStatus.Processing.ToString();
    }
}
