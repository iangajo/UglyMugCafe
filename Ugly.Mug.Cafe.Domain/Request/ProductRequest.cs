using Ugly.Mug.Cafe.Domain.Entity;

namespace Ugly.Mug.Cafe.Domain.Request
{
    public class ProductRequest : Product
    {
        public int Quantity { get; set; }
    }
}
