using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ugly.Mug.Cafe.Core.Service;
using Ugly.Mug.Cafe.Data;
using Ugly.Mug.Cafe.Domain.Entity;
using Ugly.Mug.Cafe.Domain.Enum;
using Ugly.Mug.Cafe.Domain.Request;
using Ugly.Mug.Cafe.Domain.Response;

namespace Ugly.Mug.Cafe.Core.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PushService _pushService;
        public OrderRepository(ApplicationDbContext dbContext, PushService pushService)
        {
            _dbContext = dbContext;
            _pushService = pushService;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrders()
        {
            var orders = await _dbContext.Orders.AsNoTracking().OrderByDescending(o => o.OrderDate).ToListAsync();

            if (!orders.Any()) return new List<OrderResponse>();

            return orders.Select(o => new OrderResponse()
            {
                OrderId = o.OrderId,
                OrderNumber = o.OrderNumber,
                Customer = o.Customer,
                Orders = JsonConvert.DeserializeObject<List<ProductResponse>>(o.Request),
                OrderDate = o.OrderDate,
                Status = o.Status

            }).ToList();
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByCustomerName(string customer)
        {
            var orders = await _dbContext.Orders.Where(o => o.Customer == customer && o.Status == OrderStatus.Processing.ToString()).OrderByDescending(o => o.OrderDate).AsNoTracking().ToListAsync();

            if (!orders.Any()) return new List<OrderResponse>();

            return orders.Select(o => new OrderResponse()
            {
                OrderId = o.OrderId,
                OrderNumber = o.OrderNumber,
                Customer = o.Customer,
                Orders = JsonConvert.DeserializeObject<List<ProductResponse>>(o.Request),
                OrderDate = o.OrderDate,
                Status = o.Status

            }).ToList();
        }

        public async Task<IEnumerable<ProductResponse>> GetOrdersByOrderNumber(Guid orderNumber)
        {
            var products = await _dbContext.Products.AsNoTracking().ToListAsync();

            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber
                                                                         && o.Status == OrderStatus.Processing.ToString());

            if (order == null)
            {
                return products.Select(p => new ProductResponse()
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Quantity = 0

                }).ToList();

            }

            var orderAsObject = JsonConvert.DeserializeObject<List<ProductResponse>>(order.Request);

            return products.Select(p => new ProductResponse()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Quantity = orderAsObject.Any(o => o.ProductId == p.ProductId) ? orderAsObject.First(o => o.ProductId == p.ProductId).Quantity : 0

            }).ToList();
        }

        public async Task<BaseResponse<bool>> AddOrder(Order order)
        {
            await _dbContext.AddAsync(order);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new BaseResponse<bool>()
                {
                    Result = false,
                    ErrorMessage = "Something went wrong.",
                    StatusCode = ResultType.Error
                };
            }

            _pushService.Push(new PushServiceRequest()
            {
                Type = "Add",
                Payload = $"{order.Customer}'s order has been added to queue."
            });

            return new BaseResponse<bool>()
            {
                Result = true,
                StatusCode = ResultType.Created
            };
        }

        public async Task<BaseResponse<bool>> ModifyOrder(string request, Guid orderNumber)
        {
            var orders = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (orders == null)
            {
                return new BaseResponse<bool>()
                {
                    ErrorMessage = "Unable to process your request.",
                    Result = false,
                    StatusCode = ResultType.Error,
                };
            }

            try
            {
                orders.Request = request;

                _dbContext.Orders.Update(orders);

                await _dbContext.SaveChangesAsync();

                _pushService.Push(new PushServiceRequest()
                {
                    Type = "Update",
                    Payload = $"{orders.Customer}'s order has been updated."
                });
            }
            catch (Exception)
            {
                return new BaseResponse<bool>()
                {
                    ErrorMessage = "Something went wrong.",
                    Result = false,
                    StatusCode = ResultType.Error,
                };
            }

            return new BaseResponse<bool>()
            {
                Result = true,
                StatusCode = ResultType.Success
            };
        }

        public async Task<BaseResponse<bool>> CancelOrder(Order order)
        {
            _dbContext.Orders.Remove(order);

            try
            {
                await _dbContext.SaveChangesAsync();

                _pushService.Push(new PushServiceRequest()
                {
                    Type = "Cancel",
                    Payload = $"{order.Customer}'s, order has been cancelled."
                });
            }
            catch (Exception)
            {
                return new BaseResponse<bool>()
                {
                    Result = false,
                    ErrorMessage = "Something went wrong.",
                    StatusCode = ResultType.Error
                };
            }


            return new BaseResponse<bool>()
            {
                Result = true,
                StatusCode = ResultType.Success
            };
        }

        public async Task<BaseResponse<bool>> ProcessOrder(Guid orderNumber)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (order == null) return new BaseResponse<bool>();

            try
            {
                order.Status = OrderStatus.ReadyForPickup.ToString();

                _dbContext.Orders.Update(order);

                await _dbContext.SaveChangesAsync();

                _pushService.Push(new PushServiceRequest()
                {
                    Type = "Processed",
                    Payload = $"{order.Customer}'s, order has been completed."
                });
            }
            catch (Exception)
            {
                return new BaseResponse<bool>()
                {
                    Result = false,
                    ErrorMessage = "Something went wrong.",
                    StatusCode = ResultType.Error
                };
            }


            return new BaseResponse<bool>()
            {
                Result = true,
                StatusCode = ResultType.Success
            };
        }

        public async Task<BaseResponse<bool>> CancelOrderByOrderNumber(Guid orderNumber)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            if (order == null) return new BaseResponse<bool>();

            try
            {
                order.Status = OrderStatus.Cancelled.ToString();

                _dbContext.Orders.Update(order);

                await _dbContext.SaveChangesAsync();

                _pushService.Push(new PushServiceRequest()
                {
                    Type = "Cancel",
                    Payload = $"{order.Customer}'s, order has been cancelled."
                });
            }
            catch (Exception)
            {
                return new BaseResponse<bool>()
                {
                    Result = false,
                    ErrorMessage = "Something went wrong.",
                    StatusCode = ResultType.Error
                };
            }


            return new BaseResponse<bool>()
            {
                Result = true,
                StatusCode = ResultType.Success
            };
        }
    }
}
