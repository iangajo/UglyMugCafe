using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ugly.Mug.Cafe.Domain.Entity;
using Ugly.Mug.Cafe.Domain.Response;

namespace Ugly.Mug.Cafe.Core.Orders
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderResponse>> GetAllOrders();
        Task<IEnumerable<OrderResponse>> GetOrdersByCustomerName(string customer);
        Task<IEnumerable<ProductResponse>> GetOrdersByOrderNumber(Guid orderNumber);
        Task<BaseResponse<bool>> AddOrder(Order order);
        Task<BaseResponse<bool>> ModifyOrder(string request, Guid orderNumber);
        Task<BaseResponse<bool>> CancelOrder(Order order);
        Task<BaseResponse<bool>> ProcessOrder(Guid orderNumber);
        Task<BaseResponse<bool>> CancelOrderByOrderNumber(Guid orderNumber);
    }
}
