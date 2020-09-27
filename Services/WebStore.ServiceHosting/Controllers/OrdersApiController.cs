using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService orderService;

        public OrdersApiController(IOrderService orderService) => this.orderService = orderService;

        [HttpPost("{userName}")]
        public Task<OrderDTO> CreateOrder(string userName, [FromBody] CreateOrderModel orderModel)
        {
            return orderService.CreateOrder(userName, orderModel);
        }

        [HttpGet("{id}")]
        public Task<OrderDTO> GetOrderById(int id)
        {
            return orderService.GetOrderById(id);
        }

        [HttpGet("user/{userName}")]
        public Task<IEnumerable<OrderDTO>> GetUserOrders(string userName)
        {
            return orderService.GetUserOrders(userName);
        }
    }
}
