using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ugly.Mug.Cafe.Core.Orders;
using Ugly.Mug.Cafe.Domain.Request;

namespace Ugly.Mug.Cafe.API.Controllers
{
    public class OrderController : UglyMugCafeBaseController
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        [HttpGet("v1/order/all")]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _orderRepository.GetAllOrders();

            return Ok(response);
        }


        [HttpGet("v1/order/{customer}")]
        public async Task<IActionResult> GetOrder(string customer)
        {
            var response = await _orderRepository.GetOrdersByCustomerName(customer);

            return Ok(response);
        }

        [HttpGet("v1/order")]
        public async Task<IActionResult> GetOrderByOrderNumber([FromQuery]Guid orderNumber)
        {
            var response = await _orderRepository.GetOrdersByOrderNumber(orderNumber);

            return Ok(response);
        }

        [HttpPost("v1/order/add")]
        public async Task<IActionResult> AddOrders([FromBody] AddOrderRequest request)
        {
            var orderRequest = Helper.OrderHelper.Transform(request);
            var response = await _orderRepository.AddOrder(orderRequest);

            return StatusCodeReturn(response);
        }


        [HttpPut("v1/order/update")]
        public async Task<IActionResult> UpdateOrders([FromBody] UpdateOrderRequest request)
        {
            var orderRequest = Helper.OrderHelper.Transform(request);

            var response = await _orderRepository.ModifyOrder(orderRequest.Request, orderRequest.OrderNumber);

            return StatusCodeReturn(response);
        }

        [HttpPut("v1/order/process")]
        public async Task<IActionResult> UpdateOrder([FromQuery]Guid orderNumber)
        {
            var response = await _orderRepository.ProcessOrder(orderNumber);

            return StatusCodeReturn(response);
        }

        [HttpPut("v1/order/cancel")]
        public async Task<IActionResult> CancelOrder([FromQuery]Guid orderNumber)
        {
            var response = await _orderRepository.CancelOrderByOrderNumber(orderNumber);

            return StatusCodeReturn(response);
        }
    }
}