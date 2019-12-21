using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ugly.Mug.Cafe.Domain.Entity;
using Ugly.Mug.Cafe.Domain.Response;

namespace Ugly.Mug.Cafe.Core.Orders
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderResponse>> All();
        Task<IEnumerable<OrderResponse>> Get(string customer);
        Task<IEnumerable<ProductResponse>> Get(Guid orderNumber);
        Task<BaseResponse<bool>> Add(Order order);
        Task<BaseResponse<bool>> Update(string request, Guid orderNumber);
        Task<BaseResponse<bool>> Cancel(Order order);
        Task<BaseResponse<bool>> Process(Guid orderNumber);
        Task<BaseResponse<bool>> Cancel(Guid orderNumber);
    }
}
