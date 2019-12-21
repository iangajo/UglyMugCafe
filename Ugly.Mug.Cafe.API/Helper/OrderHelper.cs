using Newtonsoft.Json;
using System;
using System.Linq;
using Ugly.Mug.Cafe.Domain.Entity;
using Ugly.Mug.Cafe.Domain.Request;

namespace Ugly.Mug.Cafe.API.Helper
{
    public static class OrderHelper
    {
        public static Order Transform(AddOrderRequest request)
        {
            var orderNumber = Guid.NewGuid();
            var customer = request.Customer;

            if (request.Products.Any())
            {
                return new Order()
                {
                    Customer = customer,
                    OrderNumber = orderNumber,
                    Request = JsonConvert.SerializeObject(request.Products),
                    OrderDate = DateTime.UtcNow
                };
            }

            return new Order();
        }

        public static Order Transform(UpdateOrderRequest request)
        {
            var orderNumber = request.OrderNumber;
            var customer = request.Customer;

            if (request.Products.Any())
            {
                return new Order()
                {
                    Customer = customer,
                    OrderNumber = orderNumber,
                    Request = JsonConvert.SerializeObject(request.Products),
                    OrderDate = DateTime.UtcNow
                };
            }

            return new Order();
        }
    }
}
